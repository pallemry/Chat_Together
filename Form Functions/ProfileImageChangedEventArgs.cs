using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form_Functions
{
    public class ProfileImageChangedEventArgs : EventArgs
    {
        public Image? NewProfileImage { get; set; }
        public string UserName { get; set; }
        public ProfileImageChangedEventArgs(Image? newProfileImage, string userName)
        {
            NewProfileImage = newProfileImage;
            UserName = userName;
        }
    }
}
