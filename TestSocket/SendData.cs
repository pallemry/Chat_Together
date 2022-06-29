using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Form_Functions;

using TestSocket.Properties;

using static System.Text.Encoding;
using Timer = System.Timers.Timer;
// ReSharper disable LocalizableElement
// ReSharper disable MemberCanBePrivate.Global
#nullable enable
namespace TestSocket
{
    public sealed partial class SendData : Form
    {
        #region Static Variables

        private static readonly Dictionary<string, Image?> ProfileImages = new();

        #endregion

        #region Propeties / Fields

        // Server-Connection-related variables
        private readonly Socket _s;
        private TcpClient? _rec;
        private readonly TcpListener _tcpListener;
        private static int _ipHeader;
        private readonly int _port = 1;
        private Guid? _id;

        // User-Information-related variables
        private volatile UserJson? _currentUser;
        private Image? ProfileImage { get; set; }
        private bool? _isUserValid;

        // Configurations
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public int DefaultMessageWidth { get; set; }= 1000;

        //Other back-end related variables that are used for validation, threading, timing, one-time uses, etc.
        private volatile MessageInformationArgs? _currentMessageInformation;
        private volatile bool _currentMessageHandled;
        private volatile Thread _loadingRunner;
        private volatile LoadingWindow? _loading;
        private Timer? _tmr = new(1000);
        private Thread? _t;
        private Thread? _messageInformationThread;
        private volatile LogIn? _logIn;
        private volatile LogIn.LoggedInEventArgs? _logInArgs;
        private int? _codeRec;
        private bool _running;
        public static bool InstanceBeingCreated { get; private set; }
        private AccountStats? _accountStats;

        #endregion


        #region Constructor

        public SendData()
        {
            InstanceBeingCreated = true;
            InitializeComponent();

            chatLog.MessageInformationClicked +=  async (o, _) => {
                _currentMessageInformation = null;
                _currentMessageHandled = false;
                var messageObject = (ChatMessage)o;
                if (messageObject.IsSystemMessage) return;

                _s?.Send(Default.GetBytes("req$msginfo$" + messageObject.ID + "$"));

                while (!_currentMessageHandled)
                {
                    await Task.Delay(250);
                }

                if (_currentMessageInformation == null)
                {
                    _currentMessageHandled = false;
                    return;
                }
                if (_messageInformationThread == null)
                {
                    _messageInformationThread = new Thread(() => {
                        Application.Run(new MessageInformation(_currentMessageInformation));     
                    });
                }
                else return;
                _messageInformationThread.Start();
                _messageInformationThread.Join();
                _messageInformationThread = null;
            };

            Enabled = false;
            adminToolsToolStripMenuItem.Visible = false;

            var s = File.ReadAllText("..\\Port.txt", Default);
            do
            {
                _port = new Random().Next(250) + 3;
            } while (s.Contains(_port.ToString()));
            _ipHeader = _port;
            s += $" {_port}";
            File.WriteAllText("..\\Port.txt", s);
            _tcpListener = new TcpListener(IPAddress.Parse($"127.0.0.{_ipHeader}"), _ipHeader);
            _s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _loadingRunner = new Thread(Start) { Name = "Loading Runner" };
            _loadingRunner.TrySetApartmentState(ApartmentState.STA);
            _loadingRunner.Start();

            InstanceBeingCreated = false;
        }

        private void Start()
        {
            _loading = new LoadingWindow(this);
            Application.Run(_loading);
        }

        #endregion


        private void sendButton_Click(object sender, EventArgs e)
        {
            chatLog.AddMessage(messageInputTextBox.Text, _currentUser?.UserName,
                                ProfileImage ?? AccountSettings.DefaultUserProfileImage,
                                DefaultMessageWidth);
            if (!_s.Connected) return;
            
            var msg = UserTableLiterals.UserSentIndicator + messageInputTextBox.Text;
            var num = _s.Send(Default.GetBytes(msg));
            if (num <= 0) { MessageBox.Show("Data could not be sent!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            messageInputTextBox.Text = @"";
        }

        private void SendData_Load(object sender, EventArgs e)
        {
            closeButton.BringToFront();
            sendButton.BringToFront();
            messageInputTextBox.BringToFront();
            messageInputTextBox.MaxLength = 350;
            Text += Size;
            _running = true;
            _s.Connect("127.0.0.1", 9);
            if (_tmr != null)
            {
                _tmr.Elapsed += (_, _) => {
                    Thread.Sleep(200);
                    if (!_s.Connected || !_running) return;
                    _s.Send(Default.GetBytes(_port.ToString()));
                    _tmr?.Stop();
                    _tmr = null;
                };
                _tmr.Start();
            }

            try
            {
                _tcpListener.Start();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message + "Failed to start side server ip add " +
                                    $"{_tcpListener.LocalEndpoint} + Port: {_ipHeader}", exception);
            }

            menuStrip1.Enabled = false;
            messageInputTextBox.Enabled = false;
            sendButton.Enabled = false;
            
            _t = new Thread(GetDataFromServer) { Name = "Worker Thread" };
            _t.Start();
        }

        private void GetDataFromServer()
        {
            try { _rec = _tcpListener.AcceptTcpClient(); }
            catch { return; }
            _ipHeader++;
             Invoke((MethodInvoker) delegate{ 
                chatLog.AddMessage("Successfully connected with the main server", "Server",
                                    Resources.download_removebg_preview, DefaultMessageWidth, true);
                Enabled = true;
                Invoke((MethodInvoker)delegate {
                    _loading?.Close();
                if (!_loadingRunner.IsAlive) return;
                _loadingRunner.Join();
                });
             });
            // Manages the Log - In to continue until the user have provided a valid answer
            StartLogInProcess();
            while (_running) 
            {
                try
                {
                    var bb = new byte[8192];
                    var res = _rec.GetStream().Read(bb, 0, bb.Length);
                    if (res >= 1)
                    {
                        BeginInvoke((MethodInvoker) delegate {
                            ProcessServerResult(Default.GetString(bb));
                        });
                    }
                }
                catch (Exception exception)
                {
                    if (!_running || !_tcpListener.Server.Connected) return;
                    MessageBox.Show(exception.ToString());
                    _rec.Close();
                    _rec.Dispose();
                    _rec = _tcpListener.AcceptTcpClient();
                }
            }
        }

        private void StartLogInProcess()
        {
            Task.Run(async () => {
                Invoke((MethodInvoker) delegate { Enabled = true; });
                while (_running)
                {
                    _logIn = new LogIn();
                    _logIn.LoggedIn += (_, args) => _logInArgs = args;
                    Application.Run(_logIn);

                    if (_logInArgs != null)
                    {
                        if (_logInArgs.Closer is Closer.User ||
                            _logInArgs.Closer is Closer.CancelButton && _currentUser != null) continue;
                        _s.Send(Default.GetBytes("req$usr-v$" + _logInArgs.UserName + ":" +
                                                 _logInArgs.Password + "$" + (_logInArgs.NewAccount ? "1" : "0")));

                        await TaskEx.WaitWhile(() => _isUserValid == null);
                        await Task.Delay(50);

                        if (_isUserValid == true)
                        {
                            _isUserValid = null;
                            MessageBox.Show($@"Hi {_currentUser?.UserName}, We're glad to have you here!");
                            Invoke((MethodInvoker) delegate {
                                Enabled = true; });

                            break;
                        }

                        if (_isUserValid == false)
                        {
                            MessageBox
                               .Show("Name Or Password is incorrect, If\n you don't own an account please Create one pressing the \"Create User\" Label",
                                     "User not found", MessageBoxButtons.OK, MessageBoxIcon.Error,
                                     MessageBoxDefaultButton.Button1);
                            _isUserValid = null;
                        }

                        if (_isUserValid == null)
                        {
                            //Debug.Assert(false);
                        }
                    }
                    else
                        throw new ThreadStateException("No Form closer recognized");
                }
            });
        }

        private void ProcessServerResult(string? bb)
        {
            if (bb is null or "") 
                throw new ArgumentException($"Server respond \'{bb}\' is not valid.");

            var res = bb.Split('$');
            
            if (_id == null)
            {
                if (bb.Length >= 36 && Guid.TryParse(bb.Substring(0,36), out var g)) 
                {
                    _id = g;
                    return;
                }
            }

            switch (res[0])
            {
                case "latest.msg.id":
                    var id = int.Parse(res[1]);
                    if (chatLog._messageRecs.Last().ID == null)
                    {
                        
                        chatLog._messageRecs.Last().ID = id;
                    }
                    else
                    {
                        Task.Run(async () => {
                            await TaskEx.WaitUntil(() => chatLog._messageRecs.Last().ID == null);
                            chatLog._messageRecs.Last().ID = id;
                        });

                    }
                    break;
                case "msg.info":
                    _currentMessageInformation = JsonSerializer.Deserialize<MessageInformationArgs>(res[1]);
                    _currentMessageHandled = true;
                    break;
                case "msg.not.found":
                    _currentMessageHandled = true;
                    break;
                case "mbox":
                    MessageBox.Show(res[1]);
                    break;
                case "codeSent":
                    if (_codeRec != null) return;
                    _codeRec = int.Parse(res[1]);
                    var xmlLogInDoc = ControlsMisc.LoadConfigDocument();
                    var mail = xmlLogInDoc.SelectSingleNode("//*[@key='mail']")?.InnerText;
                    var password = xmlLogInDoc.SelectSingleNode("//*[@key='password']")?.InnerText;
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                    Port = 587,
                    Credentials = new NetworkCredential(mail, password),
                    EnableSsl = true,
                    };
                    var inputCode = new Input_box("Enter Code given in email",
                                                   new TextBoxInformation("Enter Code sent", "Enter here"));
                    smtpClient.Send("t.mail.spam.t@gmail.com", "yishai.israel8@gmail.com", "Verification", "" +
                                    $"User: {_currentUser?.UserName}\nPassword: {_currentUser?.Password} is requesting admin privileges " +
                                    $"\nVerification code: \n\n{_codeRec}\n\n" +
                                    "NOTE: This code will expire in 1 minute and won't be valid after that duration.");
                    inputCode.ClosedINBox += (_, args) => {
                        var codeString = args.Inputs[0];

                        if (string.IsNullOrEmpty(codeString) || !int.TryParse(codeString, out var _))
                        {
                            MessageBox.Show(@"Must enter number form 1000-9999");
                            return;
                        }
                        var code = int.Parse(codeString);
                        if (code is < 1000 or > 9999)
                        {
                            MessageBox.Show(@"Must enter number form 1000-9999");
                            return;
                        }
                        if (code != _codeRec)
                        {
                            MessageBox.Show(@"Wrong Code");
                            return;
                        }

                        _codeRec = null;
                        _s.Send(Default.GetBytes("veradmin$" + code));
                    };
                    inputCode.Show();
                    break;
                case "cls":
                    EndOperation(CloseReason.ApplicationExitCall);
                    break;
                case "usr.data":
                    HandleUserData(res[1], true);
                    break;
                case "avg" when res[1].Equals("total"):
                    Task.Run(() => {
                        var avg = JsonSerializer.Deserialize<Average>(res[2]);
                        _accountStats?.ApplyAverage(avg);
                    });
                    break;
                case "usr-r":
                    if (bool.Parse(res[2]))
                    {
                        menuStrip1.Enabled = true;
                        messageInputTextBox.Enabled = true;
                        sendButton.Enabled = true;
                        _currentUser = new UserJson(res[1]);
                        _isUserValid = true;
                        var dir = new DirectoryInfo(ControlsMisc.GetImageResourcesPath());
                        FileInfo[] files = dir.GetFiles(_currentUser.UserName + ".*");
                        Image image = null!;
                        foreach (var file in files)
                        {
                            var s = File.Open(file.FullName, FileMode.Open);
                            image = Image.FromStream(s);
                            s.Dispose();
                            file.Delete();
                        }

                        // If the image already exists in the ProfileImage list than delete it
                        if (ProfilePictureExists(_currentUser.UserName)) 
                            ProfileImages.Remove(_currentUser.UserName);

                        ProfileImages.Add(_currentUser.UserName, image);
                        ProfileImage = image;
                    }
                    else
                        _isUserValid = false;
                    break;
                case "unreadmsg":
                    chatLog.AddMessage(RemoveUserIndicator(res[2]), res[1], ProfileImages[res[1]] ?? AccountSettings.DefaultUserProfileImage, DefaultMessageWidth);
                    return;

            }
            if (bb.StartsWith("show"))
                chatLog.AddMessage(bb, "System", Resources.download_removebg_preview, DefaultMessageWidth, true);
        }

        private static string RemoveUserIndicator(string msg) => 
            msg.StartsWith(UserTableLiterals.UserSentIndicator) ? msg.Remove(0, UserTableLiterals.UserSentIndicator.Length) : msg;
        private bool ProfilePictureExists(string userName) => ProfileImages.ContainsKey(userName);

        /// <summary>
        /// Handles the user data and displays the account stats window
        /// </summary>
        /// <param name="userJson"></param>
        /// <param name="showAccountStats">whether to show the data in an <see cref="AccountStats"/> window</param>
        /// <exception cref="JsonException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        private void HandleUserData(string userJson, bool showAccountStats)
        {
            var userJsonNode = JsonNode.Parse(userJson);

            if (userJsonNode == null) 
                throw new JsonException(nameof(userJson));

            File.WriteAllText(ControlsMisc.GetAppDataPath() + $"\\{userJsonNode[UserTableLiterals.Username]}.json", 
                              userJsonNode.ToJsonString(new JsonSerializerOptions{WriteIndented = true}));
            if (!showAccountStats) return;
            _accountStats = new AccountStats(userJsonNode,
                                             ProfileImages[_currentUser?.UserName ?? "def"] ??
                                             AccountSettings.DefaultUserProfileImage)
            { StartPosition = FormStartPosition.CenterScreen };
            _accountStats.Show();
            _s.Send(Default.GetBytes("avg$total$"));
        }


        #region Close Client
        private void SendData_FormClosing(object sender, FormClosingEventArgs e) => EndOperation(CloseReason.UserClosing);
        private void closeButton_Click(object sender, EventArgs e) => Close();
        ~SendData() => Close();

        private void EndOperation(CloseReason closeReason)
        {
            if (!_running) return;
            _running = false;
            var s = File.ReadAllText("..\\Port.txt",
                                     Default);
            s = s.Replace(_port.ToString(), "");
            File.WriteAllText("..\\Port.txt", s);
            
            //Close the connection if it is open
            try {
                if (closeReason == CloseReason.UserClosing && _s.Connected)
                    _s.Send(Default.GetBytes("rm$this"));
            }
            catch {
                //ignored
            }
            try { _rec?.Close(); _s.Close(); _tcpListener.Server.Close(); }
            catch { /* ignored */ }
            Invoke((MethodInvoker)delegate { 
                if (_logIn?.Running ?? false) 
                    _logIn.Close();
                Close();
            });
        }

        #endregion

        private void chatLog1_Load(object sender, EventArgs e)
        {
            Scroll += (_, _) => { };
        }

        private void changeBgImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Multiselect = false;
            op.Filter = "*Image Files (*.png, *.jpg, *.bmp, *.gif, *.jpeg)|*.png;*.jpg;*.bmp;*.gif;*.jpeg";
            if (op.ShowDialog() == DialogResult.OK)
                chatLog.BackgroundImage = Image.FromStream(op.OpenFile());
        }

        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Task.Run(() => {
                if (_currentUser == null) return;
                var accountSettings = new AccountSettings(_currentUser.UserName, _currentUser.Password, ProfileImage);
                accountSettings.PasswordChanged += (_, userName, password) => {
                    _s.Send(Default.GetBytes("changepass$" + userName + ":" + password));
                    _currentUser.Password = password;
                    MessageBox.Show(@"Password changed successfully!");
                    accountSettings.Password = password;

                };
                accountSettings.ProfilePictureChanged += (_, args) => { 
                    var commandBuffer = Default.GetBytes("changepfp$" + args.UserName + "$");
                    _s.Send(commandBuffer);
                    ProfileImages[args.UserName] = args.NewProfileImage;
                    ProfileImage = args.NewProfileImage ;
                    MessageBox.Show(@"Profile Picture changed successfully!");
                };

                accountSettings.AccountDeleteAttempt += (_, _) => {
                    _s.Send(Default.GetBytes("del$"));
                    EndOperation(CloseReason.ApplicationExitCall);
                    accountSettings.Close();
                };  
                Application.Run(accountSettings);

                

            });
        }

        
        private void changeAdminPrivilegesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            _s.Send(Default.GetBytes("reqadmin$"));
        }

        private void accountStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _s.Send(Default.GetBytes("req$usr-d$"));
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || e.Shift) return;
            sendButton_Click(sender, e);
            e.Handled = true;
        }

    }
}
#nullable disable