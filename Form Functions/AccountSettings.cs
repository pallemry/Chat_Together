using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_Functions
{
    public partial class AccountSettings : Form
    {
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                UserNameLabel.Text = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password == null)
                {
                    _password = value;
                    return;
                }

                Invoke((MethodInvoker) delegate {
                    PasswordLabel.Text = "";
                    for (int i = 0; i < value.Length; i++)
                    {
                        PasswordLabel.Text += "*";
                    }
                });
                
            }
        }

        private string _password;
        private string _userName;
        public string ImageURI { get; set; }
        public event Action<object, string, string> PasswordChanged = null!;
        private void EditPasswordLabel_Click(object sender, EventArgs e)
        {
            var ip = new Input_box("Change Password",
                                   new TextBoxInformation("Previous password", "Enter previous pass"),
                                   new TextBoxInformation("New Password", "Enter new pass"),
                                   new TextBoxInformation("Re-Enter new Password", "Confirm New Password"));
            ip.ClosedINBox += (sender, args) => {
                if (args.Inputs.Length < 3 || (args.Inputs[1] == null|| args.Inputs[2] == null))
                {
                    MessageBox.Show("Must Fill all fields.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (!args.Inputs[0].Equals(Password)) {
                    MessageBox.Show("Couldn't confirm your password\nError: You entered a wrong password",
                                                                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!args.Inputs[1].Equals(args.Inputs[2]))
                {
                    MessageBox.Show("Couldn't confirm your new password\nError: You entered a different new password" +
                                    " in each field.\nNew Passwords must be the same",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                PasswordChanged(this, UserName, args.Inputs[1]);
            };
            Task.Run(() => {
                Application.Run(ip);
            });
        }

        public AccountSettings(string userName, string password, string imageUri)
        {
            InitializeComponent();
            UserName = userName;
            Password = password;
            ImageURI = imageUri;
            PasswordLabel.Text = "";
            UserNameLabel.Text = userName;
            EditPasswordLabel.Text = @"Edit..";
            EditPasswordLabel.ForeColor = Color.Gray;
            EditPasswordLabel.InitializeLinkLabel(FontStyle.Underline, EditPasswordLabel.Font.Size, Color.RoyalBlue);
            ChangeImageButtonLabel.InitializeLinkLabel(FontStyle.Underline, EditPasswordLabel.Font.Size, Color.RoyalBlue);
            UserPicutreBox.BackgroundImage = OvalImage(UserPicutreBox.BackgroundImage);
            for (int i = 0; i < password.Length; i++)
            {
                PasswordLabel.Text += "*";
            }


        }
        public static Image OvalImage(Image img)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, img.Width + 3, img.Height + 3);
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.SetClip(gp);
                    gr.DrawImage(img, Point.Empty);
                }
            }
            return bmp;
        }

        private void ChangeImageButtonLabel_Click(object sender, EventArgs e)
        {
            Input_box imageURI = new Input_box("Change Profile Image",
                                               new TextBoxInformation("New Image URL",
                                                                      "Paste new image url here"));
        }
    }
}
