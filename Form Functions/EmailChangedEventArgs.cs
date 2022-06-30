using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form_Functions
{
    public class EmailChangedEventArgs : EventArgs
    {
        /// <inheritdoc />
        public EmailChangedEventArgs(string newEmailAddress, string? originalEmailAddress)
        {
            NewEmailAddress = newEmailAddress;
            OriginalEmailAddress = originalEmailAddress;
        }
        public string NewEmailAddress { get; set; }
        public string? OriginalEmailAddress { get; set; }
    }
}
