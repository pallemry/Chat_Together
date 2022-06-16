using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form_Functions.src
{
    public class MessageInformationArgs
    {
        public string Message { get; }
        public DateTime? DateSent { get; }
        public string UserName { get; }
        public bool IsUserAdmin { get; }
        public DateTime? UserRegistrationDate { get; }

        public MessageInformationArgs(string message, DateTime? dateSent, string userName,
                                      bool isUserAdmin, DateTime? userRegistrationDate)
        {   
            Message = message;
            DateSent = dateSent;
            UserName = userName;
            IsUserAdmin = isUserAdmin;
            UserRegistrationDate = userRegistrationDate;
        }
    }
}
