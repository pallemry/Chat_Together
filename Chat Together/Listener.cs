using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Net.Sockets;

namespace Chat_Together
{
    public class Listener
    {
        [NotMapped]
        private Socket soc { get; set; }
        public bool Listening { get; set; }
        private int Port { get; set; }
        public List<Client> ConnectedClients { get; }
        public Client LatestConnected { get; private set; }

        public Listener(int port)
        {
            ConnectedClients = new List<Client>();
            Port = port;
            soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (Listening) return;
            soc.Bind(new IPEndPoint(0, Port));
            soc.Listen(0);
            soc.BeginAccept(CallBack, null);
            Listening = true;
            
        }
        public void Stop()
        {
            if (!Listening) return;
            soc.Close();
            soc.Dispose();
            soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void CallBack(IAsyncResult a)
        {
            try {
                var s = soc.EndAccept(a);
                LatestConnected = new Client(s, this);
                SocketAccepted?.Invoke(s);
                //LatestConnected.Socket.Send(Encoding.Default.GetBytes($"Added to connected clients: {LatestConnected.ID}, {LatestConnected.ep}"));
                soc.BeginAccept(CallBack, null);
            } // Try Statement
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        public event Action<Socket> SocketAccepted;
    }
}
