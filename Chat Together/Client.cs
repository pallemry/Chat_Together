using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Windows.Forms;
#nullable enable
namespace Chat_Together
{
    public class Client
    {
        public string ID { get; set; }
        public User? User { get; set; }
        public IPEndPoint ep { get; private set; }
        public Socket Socket { get; private set; }
        private ImageList il = new ();
        public Queue<Message_Record> UnreadMessages { get; }
        public Listener Listener { get; set; }
        public bool HasAdminPrivileges { get; private set; } = false;
        public Client(Socket accepted, Listener l)
        {
            il.Images.Add("test", new Icon("C:\\Users\\yisha\\source\\repos\\ChatTogether\\TestSocket" + "\\Resources\\Shopping_cart_icon.svg.ico"));
            UnreadMessages = new Queue<Message_Record>();
            Socket = accepted;
            Listener = l;
            ID = Guid.NewGuid().ToString();
            ep = (IPEndPoint) Socket.RemoteEndPoint;
            Socket.BeginReceive(new byte[ ] {0}, 0, 0, 0, CallBack, null);
        }

        private void CallBack(IAsyncResult a)
        {
            try
            {
                Socket.EndReceive(a);
                var buf = new byte[8192];
                var rec = Socket.Receive(buf, buf.Length, 0);
                if (rec < buf.Length)
                    Array.Resize(ref buf, rec);
                Received?.Invoke(this, buf);
                Listener.ConnectedClients.Add(this);
                TimesRec++;
                Socket.BeginReceive(new byte[] {0}, 0, 0, 0, CallBack, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
                Disconnected?.Invoke(this);
                Listener.ConnectedClients.Remove(this);
            }
        }

        

        public void Close()
        {
            Socket.Close();
            Socket.Dispose();
        }

        public int TimesRec { get; private set; } = 0;

        public event ClientReceivedHandler Received;
        public event ClientDisconnectedHandler Disconnected;
    }

    public delegate void ClientReceivedHandler(Client sender, byte[] data);

    public delegate void ClientDisconnectedHandler(Client sender);

    
}
