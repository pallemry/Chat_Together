using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using Form_Functions;

namespace Chat_Together
{
    public partial class Responder
    {
        public class UsrSettings : SpecificResponder
        {
            private int? _codeSent = null;
            private Timer? _codeTimer;

            /// <inheritdoc />
            public UsrSettings(Responder parent) : base(parent) { }

            public string ChangePassword(IReadOnlyList<string> command)
            {
                var user = command[1].Split(':');

                foreach (var cteUser in Parent.Cte.Users)
                {
                    if (!cteUser.UserName.Equals(user[0])) continue;
                    cteUser.Password = user[1];
                    break;
                }
                Parent.Cte.SaveChanges();
                return "Received" + command[1];
            }

            public string ChangePfp(IReadOnlyList<string> command)
            {
                byte[]? pfp = null;
                var dir = new DirectoryInfo(ControlsMisc.GetResourcesPath());
                FileInfo[] files = dir.GetFiles(command[1] + ".*");
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
                Parent.Cte.Users.ToList().First(user1 => user1.UserName == command[1]).ProfilePicture = pfp;
                Parent.Cte.SaveChanges();
                return "Done|ExitCode=0";
            }

            public string RequestAdminPrivileges(IReadOnlyList<string> command)
            {
                if (_codeSent != null) return "mbox$Code Sent is in process";
                _codeTimer = new Timer(1000 * 60);
                _codeSent = new Random().Next(1000, 9999);
                _codeTimer.Elapsed += (_, _) => {
                    _codeTimer.Stop();
                    _codeSent = null;
                };
                _codeTimer.Start();
                return "codeSent$" + _codeSent;
            }

            public string VerifyAdminPrivileges(IReadOnlyList<string> command)
            {
                if (_codeSent == int.Parse(command[1]))
                {
                    Parent.Cte.Users.First((user1 => Parent.Client.User != null && 
                                                     user1.HasAdminPrivileges == false && user1.id == Parent.Client.User.id))
                        .HasAdminPrivileges = true;
                    Parent.Cte.SaveChanges();
                    if (_codeTimer != null && _codeTimer.Enabled)
                    {
                        _codeTimer.Stop();
                        _codeSent = null;
                    }

                    return "mbox$You're an admin now!";
                }

                return Parent.DefaultResponse(command);
            }
        }
    }
}
