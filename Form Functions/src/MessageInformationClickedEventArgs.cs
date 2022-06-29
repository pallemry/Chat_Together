using System;

namespace Form_Functions
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
