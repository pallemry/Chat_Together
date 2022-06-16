using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

using Form_Functions;
using Form_Functions.src;

using Timer = System.Timers.Timer;

// ReSharper disable LocalizableElement

namespace Chat_Together
{
    /// <inheritdoc /> <summary>With on-line chat support</summary>
    [SuppressMessage("ReSharper", "ArrangeObjectCreationWhenTypeNotEvident")]
    public partial class ChatTogether : Form
    {
        private static Listener _l;
        private ChatTogtherEntities1 cte;
        private Dictionary<string, TcpClient> tcpClients;
        private Timer t;
        private List<Label> messages= new ();
        private int? codeSent;
        
       
        // Constructor
        public ChatTogether()
        {
            InitializeComponent();

            
            // Takes care of the last ports, if they were not closed properly.
            File.WriteAllText("..\\Port.txt", "");

            cte = new (); // Initialize database
            if (!Directory.Exists("C:\\Chat Together"))
            {
                Directory.CreateDirectory("C:\\Chat Together\\resources\\UserImageProfiles\\def");                                      

                try
                {
                    MessageBox.Show(cte.Users.ToList().First().UserName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                using (var m = new MemoryStream(cte.Users.Find(23)!.ProfilePicture))
                    Image.FromStream(m).Save("C:\\Chat Together\\resources\\UserImageProfiles\\def\\defaultUser.png");
            } 
           
            _l = new Listener(9);
            tcpClients = new Dictionary<string, TcpClient>();
            // This timer is responsible for sending the message back to those who got them each 2.2 seconds.
            Timer t = new(2200);
            t.Elapsed += (_, _) => {
                if (!_l.Listening) //if we're connected
                {
                    t.Stop();
                    return;
                }
                // This foreach finds all the clients who has unread messages, and responisble for sending them those messages
                foreach (var client in from client in _l.ConnectedClients 
                                       where client.Socket.Connected && tcpClients.ContainsKey(client.ID) && client.UnreadMessages.Count > 0
                                       select client)
                {
                    var tcpListener = tcpClients[client.ID];
                    if (tcpListener is null || !tcpListener.Connected)
                        return;

                    foreach (var message in client.UnreadMessages)
                    {
                        var b = Encoding.Default.GetBytes("unreadmsg$" + message.User.UserName + "$" + message.Message);
                        var stream = tcpListener.GetStream();
                        stream.Write(b, 0, b.Length);
                        stream.Flush();
                    }
                    client.UnreadMessages.Clear();
                    
                }
            };                 
            t.Start(); // start the unreadmsg timer
            
            // When a new client is getting connected...
            _l.SocketAccepted += _ => {

            var c = _l.LatestConnected; // set the last connected property to the most recent client connected
            
            // When a new client sends back a message.
            c.Received += (_, data) =>
            {
                var dataRec = Encoding.Default.GetString(data); // the data the client sent to the sever
                var dateRec = DateTime.Now; // the date-time the data has been sent
                var responseBasedOnData = GetResponseBasedOnData(dataRec, c); // the response the sever should respond
                var b = Encoding.Default.GetBytes(responseBasedOnData);
                var checkedIDs = new List<string>();
                Message_Record record = null; //The data represented as a message record in the database
                
                //Send the message to all other connected clients
                if (responseBasedOnData.Equals("Received"))
                {
                    if (c.User != null)
                    {
                        foreach (var client in _l.ConnectedClients.Where(client => c.ID != client.ID && 
                                                                             !checkedIDs.Contains(client.ID)))
                        {
                            //init message rec
                            record = new Message_Record
                            { 
                            Message = dataRec, 
                            dateOccured = dateRec,
                            UserID = c.User.id,
                            User = c.User
                            };
                            client.UnreadMessages.Enqueue(record);
                            checkedIDs.Add(client.ID);
                        }
                    }
                }

                //Thread.Sleep(1000);
                
                //If its the first time the user sends a message add the client to a list of tcpClients so we could keep track of him
                if (c.TimesRec <= 0)
                {
                    tcpClients.Add(c.ID, new TcpClient($"127.0.0.{int.Parse(dataRec)}",
                        int.Parse(dataRec)));
                }
                
                //Send the response back to the client
                try
                {
                    if (tcpClients.ContainsKey(c.ID))
                    {
                        if (tcpClients[c.ID].Connected)
                        {
                            tcpClients[c.ID].GetStream().Write(b, 0, b.Length);
                            tcpClients[c.ID].GetStream().Flush();
                        }
                        else
                        {
                            MessageBox.Show($"Client: \nID:{c.ID}\nIPv4:{c.ep.Address} Port:{c.ep.Port} " +
                                            "not connected when it should have been");
                        }
                    }
                }
                catch (Exception e)
                {
                    e.HelpLink = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
                    MessageBox.Show(e.ToString(), "error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Information
                    , MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, true);
                }
                // Adds the client to the list view (The visual window of the sever)
                if (c.TimesRec<= 0)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        var i = new ListViewItem();
                        i.Text = c.ep.ToString();
                        i.SubItems.Add(c.ID);
                        i.SubItems.Add("---");
                        i.SubItems.Add("---");
                        i.SubItems.Add(c.ep.Port.ToString());
                        i.SubItems.Add(tcpClients[c.ID].Client.RemoteEndPoint.ToString());
                        i.Tag = c;
                        clientListView.Items.Add(i);
                    });
                }
                
                //Saves the message in the database.
                Invoke((MethodInvoker)delegate {
                    if (c.User == null || !responseBasedOnData.Equals("Received")) return;
                    record = new Message_Record
                    { Message = dataRec, dateOccured = dateRec, UserID = c.User.id };
                    for (var i = 0; i < clientListView.Items.Count; i++)
                    {
                        if (!c.ID.Equals(clientListView.Items[i].SubItems[1].Text)) continue;
                        cte.Message_Records.Add(record);
                        clientListView.Items[i].SubItems[2].Text = dataRec;
                        clientListView.Items[i].SubItems[3].Text = dateRec.ToString(CultureInfo.DefaultThreadCurrentCulture);
                        //MessageBox.Show(cte.Message_Records.ToList().Last().MessageRec.ToString());
                        try
                        {
                            cte.SaveChanges();
                        }
                        catch
                        {
                            if (tcpClients.ContainsKey(c.ID) || tcpClients[c.ID].Client.Connected) return;
                            tcpClients[c.ID].Client.Send(Encoding.Default.GetBytes("Failed to load on database"));
                        }
                        try
                        {
                            if (tcpClients.ContainsKey(c.ID))
                            {
                                if (tcpClients[c.ID].Connected)
                                {
                                    var latestMessageId = cte.Message_Records.ToList().Last().MessageID;
                                    var msgIdBuffer = Encoding.Default.GetBytes("latest.msg.id$" + 
                                                                                latestMessageId + "$");

                                    foreach (var tcpClient in tcpClients)
                                    {
                                        tcpClient.Value.GetStream().Write(msgIdBuffer, 0, msgIdBuffer.Length);
                                        tcpClient.Value.GetStream().Flush();
                                    }                                    

                                }
                                else
                                {
                                    MessageBox.Show($"Client: \nID:{c.ID}\nIPv4:{c.ep.Address} Port:{c.ep.Port} " +
                                                    "not connected when it should have been");
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            e.HelpLink = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
                        }
                        break;
                    }
                });
            };
            
            // When a client has disconnected from the main sever either by being terminated, closed or lost connection. Remove him from any list/view,
            // close all connections and release all resources related with the client
            void OnCOnDisconnected(Client sender)
            {
                Invoke((MethodInvoker) delegate {
                    if (Handle == null)
                        return;
                    try
                    {
                        tcpClients[c.ID].GetStream().Close();
                    }
                    catch
                    {
                        // ignored
                    }

                    tcpClients.Remove(c.ID);
                    for (var i = 0; i < clientListView.Items.Count; i++)
                    {
                        if (sender is { User: { } })
                        {
                            sender.User.IsOnline = false;
                        }
                        if (!c.ID.Equals(clientListView.Items[i].SubItems[1].Text)) continue;
                        clientListView.Items.RemoveAt(i);
                        break;
                    }

                    try
                    {
                        cte.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                });
            }
            c.Disconnected += OnCOnDisconnected; }; 
            _l.Start();
        }
        
        // This method gets as an input the data/message recieved from the client and the client reference itself.
        private string GetResponseBasedOnData(string data, Client c)
        {
            var serverCommand = data.Split('$');

            if (serverCommand.Length < 1) return c.TimesRec <= 0 ? "Paired Successfully" : "Received";
            Timer codeTimer = null;

            switch (serverCommand[0])
            {
                case "req":
                    switch (serverCommand[1])
                    {
                        case "id":
                            return c.ID;
                        case "msginfo":
                            var msgIdToFind = int.Parse(serverCommand[2]);
                            var msgFoundIndex = cte.Message_Records.ToList().BinarySearch(new Message_Record{MessageID = msgIdToFind}, 
                             new MessageComparer());
                            if (msgFoundIndex < 0)
                                return "msg.not.found$" + msgIdToFind;
                            var msgFound = cte.Message_Records.ToArray()[msgFoundIndex];
                            return "msg.info$" + JsonSerializer.Serialize(new MessageInformationArgs(msgFound.Message,
                                                                 msgFound.dateOccured, 
                                                                 msgFound.User.UserName,
                                                                 msgFound.User.HasAdminPrivileges,
                                                                 msgFound.User.DateCreated)) + "$";
                        case "idmsg":
                            return "not connected yet";
                        case "usr":
                            return "usr" + cte.Users.ToList()
                                              .Aggregate("$", (s, user) => s + user.UserName + ":" + user.Password + "$");
                        case "usr-p":

                            return cte.Users.ToList().Where(userToValidate => userToValidate.IsOnline).Aggregate(serverCommand[0] + "$", (s, user1) => {
                                var byteArr = user1.ProfilePicture ?? new byte[] { 0 };
                                var byteArrAsString = Encoding.Default.GetString(byteArr);
                                return s + user1.UserName + ":" + byteArrAsString + "$";
                            });
                        case "usr-v":
                            // obtain the user to check from the Client request
                            var us = serverCommand[2].Split(':');
                            bool logInAttempt(User user) => user.Password.Equals(us[1]) && user.UserName.Equals(us[0]);
                            bool newAccountAttempt(User user) => !user.UserName.Equals(us[0]);

                            switch (serverCommand.Last())
                            {
                                case "0":
                                    var logInSuccesful = false;
                                    foreach (var client in cte.Users)
                                    {
                                            
                                        //Checks to see if the user matches the name and password
                                        if (client == null || !logInAttempt(client)) continue;
                                        c.User = client;
                                        client.IsOnline = true;
                                        logInSuccesful = true;
                                        if (client.ProfilePicture == null) break;
                                        using var memoryStream = new MemoryStream(client.ProfilePicture);
                                        var image = Image.FromStream(memoryStream);
                                        using (var fileStream = File.Create($"{ControlsMisc.GetResourcesPath()}\\{client.UserName}.png"))
                                        {
                                            image.Save(fileStream, ImageFormat.Png);
                                        }
                                    }
                                    cte.SaveChanges();
                                    if (logInSuccesful) return "usr-r$" + us[0] + ":" + us[1] + "$true$";
                                    return "usr-r$" + "NaN:NaN" + "$false$";

                                case "1" when cte.Users.ToList().All(newAccountAttempt):
                                    var tempUser = new User
                                    { Password = us[1], UserName = us[0], IsOnline = true, DateCreated = DateTime.Now};
                                    c.User = tempUser;
                                    cte.Users.Add(tempUser);

                                    try
                                    {
                                        cte.SaveChanges();
                                    }
                                    catch (Exception e)
                                    {
                                        Debug.Assert(false);
                                    }
                                    return "usr-r$" + us[0] + ":" + us[1] + "$true$";

                                default:
                                    return "usr-r$" + "NaN:NaN" + "$false$";
                            }
                    }

                    break;

                case "rm" when serverCommand[1].Equals("this"):
                    OnCOnDisconnected(c);
                    c.Close();
                    return "Disconnecting..";
                //change password
                case "del":
                    OnCOnDisconnected(c);
                    if (c is { User: { } }) cte.Users.Remove(c.User);
                    cte.SaveChanges();
                    c.Close();
                    return "Disconnecting & Deleting";
                case "changepass":
                    var user = serverCommand[1].Split(':');

                    foreach (var cteUser in cte.Users)
                    {
                        if (!cteUser.UserName.Equals(user[0])) continue;
                        cteUser.Password = user[1];
                        break;
                    }
                    cte.SaveChanges();
                    return "Received" + serverCommand[1];
                case "changepfp":
                    byte[]? pfp = null;
                    var dir = new DirectoryInfo(ControlsMisc.GetResourcesPath());
                    FileInfo[] files = dir.GetFiles(serverCommand[1] + ".*");
                    string f = null;
                    foreach (var file in files)
                    {
                        var s = File.Open(file.FullName, FileMode.Open);
                        pfp = AccountSettings.ImageToByteArray(Image.FromStream(s));
                        f = file.FullName;
                        s.Dispose();
                    }
                    if (f != null)
                        File.Delete(f);
                    cte.Users.ToList().First(user1 => user1.UserName == serverCommand[1]).ProfilePicture = pfp;
                    cte.SaveChanges();
                    return "Done|ExitCode=0";
                case "reqadmin":
                    if (codeSent != null) return "mbox$Code Sent is in process";
                    codeTimer = new Timer(1000 * 60);
                    codeSent = new Random().Next(1000, 9999);
                    codeTimer.Elapsed += (_, _) => {
                        codeTimer.Stop();
                        codeSent = null;
                    };
                    codeTimer.Start();
                    return "codeSent$" + codeSent;
                case "veradmin":
                    if (codeSent == int.Parse(serverCommand[1]))
                    {
                        cte.Users.First((user1 => user1.HasAdminPrivileges == false && user1.id == c.User.id))
                           .HasAdminPrivileges = true;
                        cte.SaveChanges();
                        if (codeTimer != null && codeTimer.Enabled)
                        {
                            codeTimer.Stop();
                            codeSent = null;
                        }

                        return "mbox$You're an admin now!";
                    }
                    break;
            }
            return c.TimesRec <= 0 ? "Paired Successfully" : "Received";
        }

        private void OnCOnDisconnected(Client clientToRem)
        {
            Invoke((MethodInvoker)delegate
            {
                try
                {
                    tcpClients[clientToRem.ID].GetStream().Close();
                    tcpClients.Remove(clientToRem.ID);
                    for (var i = 0; i < clientListView.Items.Count; i++)
                    { 
                        if (clientToRem is { User: {}})
                        {
                            clientToRem.User.IsOnline = false;
                        }
                        if (!clientToRem.ID.Equals(clientListView.Items[i].SubItems[1].Text)) continue;
                        clientListView.Items.RemoveAt(i);
                        break;
                    }
                    cte.SaveChanges();
                }
                catch { /*MessageBox.Show("Could not connect to tcp client");*/ }
            });
        }
        private void Chat_Together_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText("..\\Port.txt", "");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var thread = new Thread(Start) { Name = $"Client thread {Guid.NewGuid().ToString()}" };
            thread.TrySetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private static void Start()
        {
            if (!SendData.InstanceBeingCreated)
                Application.Run(new SendData());
        }

        private void ChatTogether_Load(object sender, EventArgs e)
        {

        }

        private void terminateClient_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < clientListView.SelectedItems.Count; i++)
            {
                var clientToTerminate = clientListView.SelectedItems[i].Tag as Client;
                if (clientToTerminate is { User: { } }) clientToTerminate.User.IsOnline = false;
                cte.SaveChanges();
                tcpClients[clientToTerminate?.ID ?? throw new ArgumentNullException()].Client.Send(Encoding.Default.GetBytes("cls$"));
                Thread.Sleep(300);
                for (var ind = 0; ind < clientListView.Items.Count; ind++)
                {
                    if (!clientToTerminate.ID.Equals(clientListView.Items[i].SubItems[1].Text)) continue;
                    clientListView.Items.RemoveAt(i);
                    break;
                }
                tcpClients.Remove(clientToTerminate.ID);
                clientToTerminate.Close();
            }
        }
    }

 
}
