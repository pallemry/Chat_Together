using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Chat_Together.src.DB;

using Form_Functions;
using Form_Functions.src;

namespace Chat_Together
{
    public partial class Responder
    {
        public class Req
        {
            public Responder Parent { get; set; }
            internal Req(Responder parent)
            {
                Parent = parent;
            }

            public string UserSignIn(IReadOnlyList<string> command)
            {
                // obtain the user to check from the Client request
                var us = command[2].Split(':');
                bool logInAttempt(User user) => user.Password.Equals(us[1]) && user.UserName.Equals(us[0]);
                bool newAccountAttempt(User user) => !user.UserName.Equals(us[0]);

                switch (command.Last())
                {
                    case "0":
                        var logInSuccesful = false;
                        foreach (var client in Parent.Cte.Users)
                        {

                            //Checks to see if the user matches the name and password
                            if (client == null || !logInAttempt(client)) continue;
                            Parent.Client.User = client;
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
                        Parent.Cte.SaveChanges();
                        if (logInSuccesful) return "usr-r$" + us[0] + ":" + us[1] + "$true$";
                        return "usr-r$" + "NaN:NaN" + "$false$";

                    case "1" when Parent.Cte.Users.ToList().All(newAccountAttempt):
                        var tempUser = new User
                        { Password = us[1], UserName = us[0], IsOnline = true, DateCreated = DateTime.Now };
                        Parent.Client.User = tempUser;
                        Parent.Cte.Users.Add(tempUser);

                        try
                        {
                            Parent.Cte.SaveChanges();
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

            public string Id(IReadOnlyList<string> command) => Parent.Client.ID;

            public string Users(IReadOnlyList<string> command)
            {
                return "usr" + Parent.Cte.Users.ToList()
                                  .Aggregate("$", (s, user) => s + user.UserName + ":" + user.Password + "$");
            }

            public string UsersProfilePictures(IReadOnlyList<string> command)
            {
                return Parent.Cte.Users.ToList().Where(userToValidate => userToValidate.IsOnline).Aggregate(command[0] + "$", (s, user1) => {
                    var byteArr = user1.ProfilePicture ?? new byte[] { 0 };
                    var byteArrAsString = Encoding.Default.GetString(byteArr);
                    return s + user1.UserName + ":" + byteArrAsString + "$";
                });
            }

            /// <summary>
            /// Returns the command string that should be responded to the client
            /// </summary>
            /// <param name="serverCommand"></param>
            /// <returns></returns>
            public string MessageInfo(IReadOnlyList<string> serverCommand)
            {
                var msgIdToFind = int.Parse(serverCommand[2]);
                Message_Record msgFound;

                try { msgFound = GetMessageRecord(msgIdToFind); }
                catch { return "msg.not.found$" + msgIdToFind; }

                return "msg.info$" + JsonSerializer.Serialize(new MessageInformationArgs(msgFound.Message,
                                                               msgFound.dateOccured,
                                                               msgFound.User.UserName,
                                                               msgFound.User.HasAdminPrivileges,
                                                               msgFound.User.DateCreated)) + "$";
            }

            /// <summary>
            /// Searches for a message record using binary search. Throws a <see cref="KeyNotFoundException"/> if couldn't find an associated Message with
            /// the <paramref name="msgIdToFind"/> provided.
            /// </summary>
            /// <param name="msgIdToFind"></param>
            /// <returns></returns>
            /// <exception cref="KeyNotFoundException"></exception>
            private Message_Record GetMessageRecord(int msgIdToFind)
            {
                var msgFoundIndex = Parent.Cte.Message_Records.ToList()
                                       .BinarySearch(new Message_Record { MessageID = msgIdToFind },
                                                     new MessageComparer());
                if (msgFoundIndex < 0)
                    throw new KeyNotFoundException($"Couldn't find any message associated with the id: {msgIdToFind}");
                var msgFound = Parent.Cte.Message_Records.ToArray()[msgFoundIndex];
                return msgFound;

            }
        }

    }
}
