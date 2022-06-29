using System;
using System.Drawing;

namespace Form_Functions
{
    public class ProfileImageChangedEventArgs : EventArgs
    {
        public Image? NewProfileImage { get; }
        public string UserName { get; }
        public ProfileImageChangedEventArgs(Image? newProfileImage, string userName)
        {
            NewProfileImage = newProfileImage;
            UserName = userName;
        }
    }
}
