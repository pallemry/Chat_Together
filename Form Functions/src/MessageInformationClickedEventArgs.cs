using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form_Functions.src
{
    public class MessageInformationClickedEventArgs : EventArgs
    {
        public string OriginalSender { get; }
        public string Message { get; }

        public MessageInformationClickedEventArgs(string originalSender, string message)
        {
            OriginalSender = originalSender;
            Message = message;
        }
    }
}
