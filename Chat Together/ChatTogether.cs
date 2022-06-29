using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Chat_Together.RespondUtilities;
using Chat_Together.src.DB;
using Form_Functions;
using TestSocket;

using Timer = System.Timers.Timer;

// ReSharper disable LocalizableElement
#nullable enable
namespace Chat_Together
{
    /// <inheritdoc /> <summary>With on-line chat support</summary>
    [SuppressMessage("ReSharper", "ArrangeObjectCreationWhenTypeNotEvident")]
    public partial class ChatTogether : Form
    {
        private readonly ChatTogtherEntities1 _cte;
        private readonly Dictionary<string, TcpClient> _tcpClients;
        //private int? _codeSent;
        private Responder Responder { get; set; } = null!;

        // Constructor
        public ChatTogether()
        {
            InitializeComponent();

            // Takes care of the last ports, if they were not closed properly.
            File.WriteAllText("..\\Port.txt", "");
            _cte = new ();
            

            if (!Directory.Exists(ControlsMisc.GetImageResourcesPath() + "\\def"))
            {
                Install();
            }

            var l = new Listener(9);
            _tcpClients = new Dictionary<string, TcpClient>();
            // This timer is responsible for sending the message back to those who got them each 2.2 seconds.
            Timer t = new(2200);
            t.Elapsed += (_, _) => {
                if (!l.Listening) //if we're connected
                {
                    t.Stop();
                    return;
                }
                // This foreach finds all the clients who has unread messages, and responsible for sending them those messages
                foreach (var client in from client in l.ConnectedClients 
                                       where client.Socket.Connected && _tcpClients.ContainsKey(client.ID) && client.UnreadMessages.Count > 0
                                       select client)
                {
                    var tcpListener = _tcpClients[client.ID];
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
            l.SocketAccepted += _ => {
                var c = l.LatestConnected; // set the last connected property to the most recent client connected
                Responder = new Responder(_cte, c);
                Responder.ClientTriesToDisconnect += OnCOnDisconnected;
                // When a new client sends back a message
                c.Received += (_, data) => {
                    Responder.Client = c;
                    var dataRec = Encoding.Default.GetString(data); // the data the client sent to the sever
                    var dateRec = DateTime.Now; // the date-time the data has been sent
                    var responseBasedOnData = GetResponseBasedOnData(dataRec, c); // the response the sever should respond
                    var b = Encoding.Default.GetBytes(responseBasedOnData);
                    var checkedIDs = new List<string>();
                    Message_Record record; //The data represented as a message record in the database

                    //Send the message to all other connected clients
                    if (responseBasedOnData.Equals("Received"))
                    {
                        if (c.User != null)
                        {
                            foreach (var client in l.ConnectedClients.Where(client => c.ID != client.ID && 
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

                    //If its the first time the user sends a message add the client to a list of tcpClients so we could keep track of him
                    if (c.TimesRec <= 0)
                    {
                        _tcpClients.Add(c.ID, new TcpClient($"127.0.0.{int.Parse(dataRec)}",
                                                            int.Parse(dataRec)));
                    }


                    //Send the response given by GetResponseBasedOnData(string, Client) back to the client
                    try
                    {
                        if (_tcpClients.ContainsKey(c.ID))
                        {
                            if (_tcpClients[c.ID].Connected)
                            {
                                _tcpClients[c.ID].GetStream().Write(b, 0, b.Length);
                                _tcpClients[c.ID].GetStream().Flush();
                            }
                            else
                            {
                                MessageBox.Show($"Client: \nID:{c.ID}\nIPv4:{c.Ep.Address} Port:{c.Ep.Port} " +
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
                        Invoke((MethodInvoker)(() => { AddClientToListView(c); }));
                    }



                    //Saves the message in the database.
                    Invoke((MethodInvoker)(() => {
                        if (c.User == null || !responseBasedOnData.Equals("Received")) return;
                        record = new Message_Record { Message = SendData.RemoveUserIndicator(dataRec), dateOccured = dateRec, UserID = c.User.id };
                        for (var i = 0; i < clientListView.Items.Count; i++)
                        {
                            if (!c.ID.Equals(clientListView.Items[i].SubItems[1].Text)) continue;

                            try { _cte.Message_Records.Add(record); }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.ToString());
                            }

                            var text = dataRec.StartsWith(UserTableLiterals.UserSentIndicator) ? dataRec.Substring(2) : dataRec;
                            clientListView.Items[i].SubItems[2].Text = text;
                            clientListView.Items[i].SubItems[3].Text =      
                                dateRec.ToString(CultureInfo.DefaultThreadCurrentCulture);

                            try
                            {
                                _cte.SaveChanges();
                            }
                            catch
                            {
                                if (_tcpClients.ContainsKey(c.ID) || _tcpClients[c.ID].Client.Connected) return;
                                _tcpClients[c.ID].Client.Send(Encoding.Default.GetBytes("Failed to load on database"));
                            }

                            try
                            {
                                if (_tcpClients.ContainsKey(c.ID))
                                {
                                    if (_tcpClients[c.ID].Connected)
                                    {
                                        var latestMessageId = _cte.Message_Records.ToList().Last().MessageID;
                                        var msgIdBuffer = Encoding.Default.GetBytes("latest.msg.id$" +
                                         latestMessageId + "$");

                                        foreach (var tcpClient in _tcpClients)
                                        {
                                            tcpClient.Value.GetStream().Write(msgIdBuffer, 0, msgIdBuffer.Length);
                                            tcpClient.Value.GetStream().Flush();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Client: \nID:{c.ID}\nIPv4:{c.Ep.Address} Port:{c.Ep.Port} " +
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
                    }));
            };
                c.Disconnected += OnCOnDisconnected; };
            l.Start();
        }

        private void Install()
        {
            Directory.CreateDirectory($"{ControlsMisc.GetImageResourcesPath()}\\def");
            File.WriteAllText(ControlsMisc.LogInInformationConfigPath, "" +
@"<?xml version=""1.0"" encoding=""utf-8""?>
<Data>
    <SmtpGmail key = ""mail"">Your Smtp gmail address goes in here</SmtpGmail>
    <SmtpPassword key = ""password"">Your Password goes in here</SmtpPassword>
</Data>");
            using var m = new MemoryStream(_cte.Users.Find(23)!.ProfilePicture);
            Image.FromStream(m).Save($"{ControlsMisc.GetImageResourcesPath()}\\def\\defaultUser.png");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c">The client to add to the main listView</param>
        private void AddClientToListView(Client c)
        {
            var i = new ListViewItem { Text = c.Ep.ToString() };
            i.SubItems.Add(c.ID);
            i.SubItems.Add("---");
            i.SubItems.Add("---");
            i.SubItems.Add(c.Ep.Port.ToString());
            i.SubItems.Add(_tcpClients[c.ID].Client.RemoteEndPoint.ToString());
            i.Tag = c;
            clientListView.Items.Add(i);
        }

        // This method gets as an input the data/message received from the client and the client reference itself.
        private string GetResponseBasedOnData(string data, Client c)
        {
            var serverCommand = data.Split('$');
            if (serverCommand.Length < 1) return c.TimesRec <= 0 ? "Paired Successfully" : "Received";
            var responderMethodBasedOnCommand = Responder.GetResponseMethod(serverCommand, data);
            var finalResponse = responderMethodBasedOnCommand.Invoke(serverCommand);
            return finalResponse;
        }

        /// <summary>
        /// When a client has disconnected from the main sever either by being terminated, closed or lost connection. Remove him from any list/view,
        /// close all connections and release all resources related with the client
        /// </summary>
        /// <param name="clientToRem">The client object to remove</param>
        private void OnCOnDisconnected(Client clientToRem)
        {
            Invoke((MethodInvoker)delegate
            {
                try
                {
                    _tcpClients[clientToRem.ID].GetStream().Close();
                    _tcpClients.Remove(clientToRem.ID);
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
                    _cte.SaveChanges();
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
            var thread = new Thread(Start) { Name = $"Client thread {Guid.NewGuid()}" };
            thread.TrySetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private static void Start()
        {
            if (!SendData.InstanceBeingCreated)
                Application.Run(new SendData());
        }

        private void terminateClient_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < clientListView.SelectedItems.Count; i++)
            {
                var clientToTerminate = clientListView.SelectedItems[i].Tag as Client;
                if (clientToTerminate is { User: { } }) clientToTerminate.User.IsOnline = false;
                _cte.SaveChanges();
                _tcpClients[clientToTerminate?.ID ?? throw new ArgumentNullException()].Client.Send(Encoding.Default.GetBytes("cls$"));
                Thread.Sleep(300);
                for (var ind = 0; ind < clientListView.Items.Count; ind++)
                {
                    if (!clientToTerminate.ID.Equals(clientListView.Items[i].SubItems[1].Text)) continue;
                    clientListView.Items.RemoveAt(i);
                    break;
                }
                _tcpClients.Remove(clientToTerminate.ID);
                clientToTerminate.Close();
            }
        }
    }

 
}