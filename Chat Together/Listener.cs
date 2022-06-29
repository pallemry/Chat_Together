#nullable enable
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
        private Socket Soc { get; set; }
        public bool Listening { get; private set; }
        private int Port { get; }
        public List<Client> ConnectedClients { get; }
        public Client LatestConnected { get; private set; } = null!;

        public event Action<Socket>? SocketAccepted;

        public Listener(int port)
        {
            ConnectedClients = new List<Client>();
            Port = port;
            Soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (Listening) return;
            Soc.Bind(new IPEndPoint(0, Port));
            Soc.Listen(0);
            Soc.BeginAccept(CallBack, null);
            Listening = true;
            
        }

        // ReSharper disable once UnusedMember.Global
        public void Stop()
        {
            if (!Listening) return;
            Soc.Close();
            Soc.Dispose();
            Soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void CallBack(IAsyncResult a)
        {
            try {
                var s = Soc.EndAccept(a);
                LatestConnected = new Client(s, this);
                SocketAccepted?.Invoke(s);
                //LatestConnected.Socket.Send(Encoding.Default.GetBytes($"Added to connected clients: {LatestConnected.ID}, {LatestConnected.ep}"));
                Soc.BeginAccept(CallBack, null);
            } // Try Statement
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}
