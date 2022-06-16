using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Form_Functions;
using Form_Functions.src;

using TestSocket;
using TestSocket.Properties;

using static System.Text.Encoding;
using Timer = System.Timers.Timer;
// ReSharper disable LocalizableElement
#nullable enable
namespace Chat_Together
{
    public enum Dependency
    {
        ServerDependent,
        Independent
    }
    
    public partial class SendData : Form
    {
        #region Static Variables

        private static Dictionary<string, Image?> _profileImages = new();

        #endregion

        #region Propeties / Fields

        // Server-Connection-related variables
        private readonly Socket _s;
        private TcpClient? _rec;
        private readonly TcpListener _tcpListener;
        private static int _ipHeader;
        private readonly int _port = 1;
        public Guid? _id;

        // User-Information-related variables
        private List<string> _userNames = new(), _passwords = new ();
        private volatile User? _currentUser;
        private Image? ProfileImage { get; set; }
        private bool? _isUserValid;

        // Configurations
        public int _defaultMessageWidth = 1000;

        //Other back-end related variables that are used for validation, threading, timing, one-time uses, etc.
        private volatile MessageInformationArgs? _currentMessageInformation = null;
        private volatile bool _currentMessageHandled = false;
        private volatile Thread _loadingRunner;
        private volatile LoadingWindow? _loading;
        private Timer? _tmr = new(1000);
        private Thread? _t;
        private Thread? _messageInformationThread = null;
        private volatile LogIn? logIn;
        private volatile LogIn.LoggedInEventArgs? _logInArgs;
        private int? codeRec;
        private bool _running;
        public static bool InstanceBeingCreated { get; private set; }

        #endregion


        #region Constructor

        

        public SendData()
        {
            InstanceBeingCreated = true;
            _loadingRunner = new Thread(Start);
            _loadingRunner.Name = "Loading Runner";
            _loadingRunner.TrySetApartmentState(ApartmentState.STA);
            _loadingRunner.Start();
            InitializeComponent();
            chatLog.MessageInformationClicked +=  async (o, args) => {
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
            var r = new Random();

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
            chatLog.AddMessage(textBox1.Text, _currentUser?.Name,
                                ProfileImage ?? AccountSettings.DefaultUserProfileImage,
                                _defaultMessageWidth);
            if (!_s.Connected) return;
            var msg = textBox1.Text;
            var num = _s.Send(Default.GetBytes(msg));
            if (num <= 0) { MessageBox.Show("Data could not be sent!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            textBox1.Text = @"";
        }
        
        private void SendData_Load(object sender, EventArgs e)
        {
            closeButton.BringToFront();
            sendButton.BringToFront();
            textBox1.BringToFront();
            Text += Size;
            _running = true;
            _s.Connect("127.0.0.1", 9);

            if (_tmr != null)
            {
                _tmr.Elapsed += (_, _) => {
                    Thread.Sleep(200);
                    if (!_s.Connected || !_running) return;
                    var size = _s.Send(Default.GetBytes(_port.ToString()));
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
                throw new Exception(exception.Message + $"Failed to start side server ip add " +
                                    $"{_tcpListener.LocalEndpoint} + Port: {_ipHeader}", exception);
            }

            menuStrip1.Enabled = false;
            textBox1.Enabled = false;
            sendButton.Enabled = false;
            
            _t = new Thread(GetDataFromServer);
            _t.Name = "Worker Thread";
            _t.Start();
        }

        private void GetDataFromServer()
        {
            try { _rec = _tcpListener.AcceptTcpClient(); }
            catch { return; }
            _ipHeader++;
             Invoke((MethodInvoker) delegate{ 
                chatLog.AddMessage("Successfully connected with the main server", "Server",
                                    Resources.download_removebg_preview, _defaultMessageWidth, true);
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

        protected virtual void StartLogInProcess()
        {
            
            Task.Run(() => {
                Invoke((MethodInvoker) delegate { Enabled = true; });
                while (_running)
                {
                    logIn = new LogIn();
                    logIn.LoggedIn += (_, args) => _logInArgs = args;
                    Application.Run(logIn);

                    if (_logInArgs != null)
                    {
                        if (_logInArgs.Closer is Closer.User ||
                            _logInArgs.Closer is Closer.CancelButton && _currentUser != null) continue;
                        _s.Send(Default.GetBytes("req$usr-v$" + _logInArgs.UserName + ":" +
                                                 _logInArgs.Password + "$" + (_logInArgs.NewAccount ? "1" : "0")));

                        while (_isUserValid == null) ;
                        Thread.Sleep(50);
                        if (_isUserValid == true)
                        {
                            _isUserValid = null;
                            MessageBox.Show($"Hi {_currentUser?.Name}, We're glad to have you here!");
                            Invoke((MethodInvoker) delegate { Enabled = true; });

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
                    if (codeRec != null) return;
                    codeRec = int.Parse(res[1]);
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                    Port = 587,
                    Credentials = new NetworkCredential("t.mail.spam.t@gmail.com", "k4t%Q9SVlclm1V9c"),
                    EnableSsl = true,
                    };
                    var input_code = new Input_box("Enter Code given in email",
                                                   new TextBoxInformation("Enter Code sent", "Enter here"));
                    smtpClient.Send("t.mail.spam.t@gmail.com", "yishai.israel8@gmail.com", "Verification", "" +
                                    $"User: {_currentUser?.Name}\nPassword: {_currentUser?.Password} is requesting admin privileges " +
                                    $"\nVerification code: \n\n{codeRec}\n\nNOTE: This code will expire in 1 minute and won't be valid after that duration.");
                    Task.Run(() => Application.Run(input_code));
                    input_code.ClosedINBox += (_, args) => {
                        var codeString = args.Inputs[0];

                        if (string.IsNullOrEmpty(codeString) || !int.TryParse(codeString, out var g))
                        {
                            MessageBox.Show("Must enter number form 1000-9999");
                            return;
                        }
                        var code = int.Parse(codeString);
                        if (code is < 1000 or > 9999)
                        {
                            MessageBox.Show("Must enter number form 1000-9999");
                            return;
                        }
                        if (code != codeRec)
                        {
                            MessageBox.Show("Wrong Code");
                            return;
                        }

                        codeRec = null;
                        _s.Send(Default.GetBytes("veradmin$" + code));
                    };
                    break;
                case "cls":
                    EndOperation(CloseReason.ApplicationExitCall);
                    break;

                case "usr-r":
                    var us = res[1].Split(':');

                    if (bool.Parse(res[2]))
                    {
                        menuStrip1.Enabled = true;
                        textBox1.Enabled = true;
                        sendButton.Enabled = true;
                        _isUserValid = true;
                        _currentUser = new User(us[0], us[1]);
                        var dir = new DirectoryInfo(ControlsMisc.GetResourcesPath());
                        FileInfo[] files = dir.GetFiles(_currentUser.Name + ".*");
                        var image = (Image) null;
                        foreach (var file in files)
                        {
                            var s = File.Open(file.FullName, FileMode.Open);
                            image = Image.FromStream(s);
                            s.Dispose();
                            file.Delete();
                        }
                        if (_profileImages.ContainsKey(_currentUser.Name)) _profileImages.Remove(_currentUser.Name);
                        _profileImages.Add(_currentUser.Name, image);
                        ProfileImage = image;
                    }
                    else
                        _isUserValid = false;
                    break;
                case "unreadmsg":
                    chatLog.AddMessage(res[2], res[1], _profileImages[res[1]] ?? AccountSettings.DefaultUserProfileImage, _defaultMessageWidth);
                    return;

            }
            if (bb.StartsWith("show"))
                chatLog.AddMessage(bb, "System", Resources.download_removebg_preview, _defaultMessageWidth, true);
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
            try { _rec?.Close(); _s?.Close(); _tcpListener?.Server.Close(); }
            catch { /* ignored */ }
            Invoke((MethodInvoker)delegate { 
                if (logIn?.Running ?? false) 
                    logIn.Close();
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
                var accountSettings = new AccountSettings(_currentUser.Name, _currentUser.Password, ProfileImage);
                accountSettings.PasswordChanged += (_, userName, password) => {
                    _s.Send(Default.GetBytes("changepass$" + userName + ":" + password));
                    _currentUser.Password = password;
                    _currentUser.Name = userName;
                    MessageBox.Show("Password changed successfully!");
                    accountSettings.Password = password;

                };
                accountSettings.ProfilePictureChanged += (_, args) => { 
                    var commandBuffer = Default.GetBytes("changepfp$" + args.UserName + "$");
                    _s.Send(commandBuffer);
                    _profileImages[args.UserName] = args.NewProfileImage;
                    ProfileImage = args.NewProfileImage ;
                };

                accountSettings.AccountDeleteAttempt += (o, args) => {
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

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || e.Shift) return;
            sendButton_Click(sender, e);
            e.Handled = true;
        }

    }
}
#nullable disable