using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Together
{
    public class MessageComparer : IComparer<Message_Record>
    {
        /// <inheritdoc />
        public int Compare(Message_Record x, Message_Record y) => x.MessageID.CompareTo(y.MessageID);
    }
}
