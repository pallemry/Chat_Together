#nullable enable
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

using Chat_Together.src.DB;

// test merge.
namespace Chat_Together
{
    public delegate void ClientReceivedHandler(Client sender, byte[] data);

    public class Client
    {
        public string ID { get; }
        public int TimesRec { get; private set; }
        public User? User { get; set; }
        public IPEndPoint Ep { get; }
        public Socket Socket { get; }
        public Queue<Message_Record> UnreadMessages { get; }
        private Listener Listener { get; }

        public event ClientReceivedHandler? Received;
        public event ClientDisconnectedHandler? Disconnected;

        public Client(Socket accepted, Listener l)
        {
            UnreadMessages = new Queue<Message_Record>();
            Socket = accepted;
            Listener = l;
            ID = Guid.NewGuid().ToString();
            Ep = (IPEndPoint) Socket.RemoteEndPoint;
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
    }

    public delegate void ClientDisconnectedHandler(Client sender);

    
}
