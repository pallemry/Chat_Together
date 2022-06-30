using System;
using System.Collections.Generic;

using Chat_Together.src.DB;

using Form_Functions;

using Timer = System.Timers.Timer;

namespace Chat_Together.RespondUtilities
{
    public partial class Responder
    {
        public delegate string ResponderMethod(string[] command);


        public Req Request { get; }
        public Cls Close { get; }
        public ChatTogtherEntities1 Cte { get; }
        public UsrSettings UserSettings { get; }
        public Avg Average { get; }

        public Client Client
        {
            get
            {
                return _client;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _client = value;
            }
            
        }

        private Timer codeTimer = null;
        private int? _codeSent;
        private Client _client;

        public Responder(ChatTogtherEntities1 cte, Client client)
        {
            Cte = cte;
            Client = client;
            Request = new Req(this);
            Close = new Cls(this);
            UserSettings = new UsrSettings(this);
            Average = new Avg(this);
            BeforeClientTriesToDisconnect += OnBeforeClientTriesToDisconnect;
            }

        private void OnBeforeClientTriesToDisconnect(Client client)
        {
            if (client.User == null) return;
            client.User.TotalTicks += (DateTime.Now - client.User.LastTimeLoggedIn).Value.Ticks;
            client.User.IsOnline = false;
            Cte.SaveChangesAsync();
        }

        public event Action<Client> ClientTriesToDisconnect;
        public event Action<Client> BeforeClientTriesToDisconnect;


        public string DefaultResponse(IReadOnlyList<string> command)
        {
            if (Client.TimesRec <= 0)
                return "Paired Successfully";
            if (Client is { User: { } })
            {
                Client.User.TotalMessagesSent += 1;
            }

            Cte.SaveChangesAsync();
            return "Received";
        }

        public ResponderMethod GetResponseMethod(string[] command, string originalCommand)
        {
            // If it's a man-made request then return just the default response to prevent users from
            // sending potential harmful or sensitive requests such as "req$usr$"
            if (originalCommand.StartsWith(UserTableLiterals.UserSentIndicator))
                return DefaultResponse;

            switch (command[0])
            {
                case "avg":
                    switch (command[1])
                    {
                        case "total":
                            return Average.Total;
                    }
                    break;
                case "req":
                    switch (command[1])
                    {
                        case "id":
                            return Request.Id;
                        case "msginfo":
                            return Request.MessageInfo;
                        case "usr":
                            return Request.Users;
                        case "usr-p":
                            return Request.UsersProfilePictures;
                        case "usr-v":
                            return Request.UserSignIn;
                        case "usr-d":
                            return Request.UserData;
                    }

                    break;

                case "rm" when command[1].Equals("this"):
                    BeforeClientTriesToDisconnect?.Invoke(Client);
                    ClientTriesToDisconnect?.Invoke(Client);
                    return Close.ForceRemoveThis;

                //change password
                case "del":
                    BeforeClientTriesToDisconnect?.Invoke(Client);
                    ClientTriesToDisconnect?.Invoke(Client);
                    return Close.ForceDeleteThis;

                case "changepass":
                    return UserSettings.ChangePassword;

                case "changepfp":
                    return UserSettings.ChangePfp;

                case "reqadmin":
                    return UserSettings.RequestAdminPrivileges;

                case "changemail":
                    return UserSettings.ChangeEmailAddress;

                case "veradmin":
                    return UserSettings.VerifyAdminPrivileges;
            }

            return DefaultResponse;
        }

    }
}
