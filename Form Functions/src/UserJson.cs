using System;
using System.Text.Json.Nodes;
// ReSharper disable ArrangeAccessorOwnerBody

namespace Form_Functions
{
    public class UserJson
    {
        private JsonNode UserAsJsonDocument { get; set; }

        public string UserName
        {
            get
            {
                var userJsonNode = UserAsJsonDocument[UserTableLiterals.Username] ??
                               throw new ArgumentNullException(nameof(UserAsJsonDocument));
                return userJsonNode.ToString();
            }
        }

        public string Password
        {
            get
            {
                var userJsonNode = UserAsJsonDocument[UserTableLiterals.Password] ??
                                   throw new ArgumentNullException(nameof(UserAsJsonDocument));
                return userJsonNode.ToString();
            }
            set
            {
                UserAsJsonDocument[UserTableLiterals.Password] = value;
            }
        }

        public DateTime? DateOfRegistration
        {
            get
            {
                var userJsonNode = UserAsJsonDocument[UserTableLiterals.DateOfRegistration] ??
                                   throw new ArgumentNullException(nameof(UserAsJsonDocument));
                if (userJsonNode.ToString().ToLower().Equals("null")) return null;
                return DateTime.Parse(userJsonNode.ToString());
            }
        }

        public TimeSpan OnlineTime
        {
            get
            {
                var userJsonNode = UserAsJsonDocument[UserTableLiterals.TotalAmountOfTicksOnline] ??
                                   throw new ArgumentNullException(nameof(UserAsJsonDocument));
                return TimeSpan.FromTicks(long.Parse(userJsonNode.ToString()));
            }
        }

        public long TotalMessagesSent
        {
            get
            {
                var userJsonNode = UserAsJsonDocument[UserTableLiterals.TotalNumMessagesSent] ??
                                   throw new ArgumentNullException(nameof(UserAsJsonDocument));
                return long.Parse(userJsonNode.ToString());
            }
        }

        public bool IsOnline
        {
            get
            {
                var userJsonNode = UserAsJsonDocument[UserTableLiterals.IsOnline] ??
                                   throw new ArgumentNullException(nameof(UserAsJsonDocument));
                return bool.Parse(userJsonNode.ToString().ToLower());
            }
        }

        public bool HasAdminPrivileges
        {
            get
            {
                var userJsonNode = UserAsJsonDocument[UserTableLiterals.HasAdminPrivileges] ??
                                   throw new ArgumentNullException(nameof(UserAsJsonDocument));
                return bool.Parse(userJsonNode.ToString().ToLower());
            }
        }

        public UserJson(JsonNode userAsJson) => 
            UserAsJsonDocument = userAsJson;

        public UserJson(string userAsJsonString) => 
            UserAsJsonDocument = JsonNode.Parse(userAsJsonString) ?? throw new InvalidOperationException();
    }
}
