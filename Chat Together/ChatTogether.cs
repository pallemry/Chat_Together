using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

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
        private int? codeSent = null;
        
       
        // Constructor
        public ChatTogether()
        {
            InitializeComponent();
            
            // Takes care of the last ports, if they were not closed properly.
            File.WriteAllText("..\\Port.txt", "");

            cte = new (); // Initialize databse
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
            _l.SocketAccepted += socket => {

            var c = _l.LatestConnected; // set the last connected property to the most recent client connected
            
            // When a new client sends back a message.
            c.Received += (sender, data) =>
            {
                var dataRec = Encoding.Default.GetString(data); // the data the client sent to the sever
                var dateRec = DateTime.Now; // the date-time the data has been sent
                var responseBasedOnData = GetResponseBasedOnData(dataRec, c); // the respone th sever should respond
                var b = Encoding.Default.GetBytes(responseBasedOnData);
                var checkedIDs = new List<string>();
                Message_Record record = null; //The data represnted as a message record in the database
                
                //Send the message to all other connected clients
                foreach (var client in _l.ConnectedClients.Where(client => c.ID != client.ID && responseBasedOnData.Equals("Received") && 
                                                                           !checkedIDs.Contains(client.ID)))
                {
                    if (c.User == null) break;
                    //init mesage rec
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
                Thread.Sleep(1000);
                
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
                        var client = clientListView.Items[i].Tag as Client;
                        if (!c.ID.Equals(clientListView.Items[i].SubItems[1].Text)) continue;
                        clientListView.Items.RemoveAt(i);
                        break;
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

            if (serverCommand.Length >= 2)
            {
                Timer codeTimer = null;

                switch (serverCommand[0])
                {
                    case "req":
                        switch (serverCommand[1])
                        {
                            case "id":
                                return c.ID;

                            case "idmsg":
                                //return cte.Message_Records.ToList().Last().MessageRec.ToString();
                                return "not connected yet";
                            case "usr":
                                return "usr" + cte.Users.ToList()
                                                       .Aggregate("$", (s, user) => s + user.UserName + ":" + user.Password + "$");
                            case "usr-v":
                                // obtain the user to check from the Client request
                                var us = serverCommand[2].Split(':');
                                Func<User, bool> logInAttempt = user =>
                                    user.Password.Equals(us[1]) && user.UserName.Equals(us[0]);
                                Func<User, bool> newAccountAttempt = user => !user.UserName.Equals(us[0]);

                                switch (serverCommand.Last())
                                {
                                    case "0":
                                        foreach (var client in cte.Users)
                                        {
                                            
                                            //Checks to see if the user matches the name and password
                                            if (client == null || !logInAttempt(client)) continue;
                                            c.User = client;
                                            return "usr-r$" + us[0] + ":" + us[1] + "$true$";
                                        }
                                        return "usr-r$" + "NaN:NaN" + "$false$";

                                    case "1" when cte.Users.ToList().All(newAccountAttempt):
                                        var tempUser = new User { Password = us[1], UserName = us[0] };
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
                        var client = clientListView.Items[i].Tag as Client;
                        if (!clientToRem.ID.Equals(clientListView.Items[i].SubItems[1].Text)) continue;
                        clientListView.Items.RemoveAt(i);
                        break;
                    }
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
            Application.Run(new SendData(Dependency.ServerDependent));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var g = CreateGraphics();
            g.FillEllipse(new SolidBrush(Color.Blue), new Rectangle(0, 0, Width, Height));
            //g.DrawLine(new Pen(Color.Red), c.Location.X, c.Location.Y - 10, c.Location.X + c.Width, c.Location.Y - 10);
            g.Flush(FlushIntention.Sync);
            g.Dispose();
        }

        private void ChatTogether_Load(object sender, EventArgs e)
        {

        }

        private void terminateClient_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < clientListView.SelectedItems.Count; i++)
            {
                var clientToTerminate = clientListView.SelectedItems[i].Tag as Client;
                tcpClients[clientToTerminate?.ID ?? throw new ArgumentNullException()].Client.Send(Encoding.Default.GetBytes("cls$"));
                Thread.Sleep(300);
                for (var ind = 0; ind < clientListView.Items.Count; ind++)
                {
                    var client = clientListView.Items[i].Tag as Client;
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
