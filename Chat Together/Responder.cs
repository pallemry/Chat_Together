using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Timers;

using Form_Functions;
using Form_Functions.src;

namespace Chat_Together
{
    public partial class Responder
    {
        public delegate string ResponderMethod(string[] command);


        public Req Request { get; set; }
        public Cls Close { get; set; }
        public ChatTogtherEntities1 Cte { get; set; }
        public UsrSettings UserSettings { get; set; }

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
        }

        public event Action<Client> ClientTriesToDisconnect;


        public string DefaultResponse(IReadOnlyList<string> command) => Client.TimesRec <= 0 ? "Paired Successfully" : "Received";

        public ResponderMethod GetResponseMethod(string[] command)
        {
            switch (command[0])
            {
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
                    }

                    break;

                case "rm" when command[1].Equals("this"):
                    ClientTriesToDisconnect?.Invoke(Client);
                    return Close.ForceRemoveThis;

                //change password
                case "del":
                    ClientTriesToDisconnect?.Invoke(Client);
                    return Close.ForceDeleteThis;

                case "changepass":
                    return UserSettings.ChangePassword;

                case "changepfp":
                    return UserSettings.ChangePfp;

                case "reqadmin":
                    return UserSettings.RequestAdminPrivileges;

                case "veradmin":
                    return UserSettings.VerifyAdminPrivileges;
            }

            return DefaultResponse;
        }

    }
}
