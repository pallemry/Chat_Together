﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#nullable enable
namespace Form_Functions
{
    public partial class AccountSettings : Form
    {
        public static readonly Image DefaultUserProfileImage = Image.FromFile(ControlsMisc.GetImageResourcesPath() + "\\def\\defaultUser.png");

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
                    for (var i = 0; i < value.Length; i++)
                    {
                        PasswordLabel.Text += "*";
                    }
                });
                
            }
        }


        private string _password;
        private string _userName;
        public event Action<object, string, string> PasswordChanged = null!;
        public event EventHandler<ProfileImageChangedEventArgs> ProfilePictureChanged = null!;
        public event EventHandler<EventArgs> AccountDeleteAttempt = null!;
        public Image? Pfp { get; set; }

        public AccountSettings(string userName, string password, Image? originalPfp)
        {
            InitializeComponent();
            UserName = userName;
            Password = password;
            PasswordLabel.Text = "";
            UserNameLabel.Text = userName;
            EditPasswordLabel.Text = @"Edit..";
            EditPasswordLabel.ForeColor = Color.Gray;
            DeleteAccountLbl.ForeColor = Color.Gray;
            EditPasswordLabel.InitializeLinkLabel(FontStyle.Underline, EditPasswordLabel.Font.Size, Color.RoyalBlue);
            ChangeImageButtonLabel.InitializeLinkLabel(FontStyle.Underline, EditPasswordLabel.Font.Size, Color.RoyalBlue);
            ChangeImageByFileLbl.InitializeLinkLabel(FontStyle.Underline, EditPasswordLabel.Font.Size, Color.RoyalBlue);
            DeleteProfilePictureLbl.InitializeLinkLabel(FontStyle.Underline, EditPasswordLabel.Font.Size, Color.RoyalBlue);
            DeleteAccountLbl.InitializeLinkLabel(FontStyle.Underline, EditPasswordLabel.Font.Size, Color.RoyalBlue);
            UserPicutreBox.BackgroundImage = OvalImage(originalPfp ?? DefaultUserProfileImage);
            Pfp = originalPfp;
            for (var i = 0; i < password.Length; i++)
            {
                PasswordLabel.Text += "*";
            }


        }

        private void EditPasswordLabel_Click(object sender, EventArgs e)
        {
            var ip = new Input_box("Change Password",
                                   new TextBoxInformation("Previous password", "Enter previous pass"),
                                   new TextBoxInformation("New Password", "Enter new pass"),
                                   new TextBoxInformation("Re-Enter new Password", "Confirm New Password"));
            ip.ClosedINBox += (_, args) => {
                if (args.Inputs.Length < 3 || (args.Inputs[1] == null || args.Inputs[2] == null))
                {
                    MessageBox.Show("Must Fill all fields.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!args.Inputs[0].Equals(Password))
                {
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

        public static Image OvalImage(Image img)
        {
            var bmp = new Bitmap(img.Width, img.Height);
            using var gp = new GraphicsPath();

            gp.AddEllipse(0, 0, img.Width, img.Height);
            using var gr = Graphics.FromImage(bmp);

            gr.SetClip(gp);
            gr.DrawImage(img, Point.Empty);

            return bmp;
        }

        private void ChangeImageButtonLabel_Click(object sender, EventArgs e)
        {
            var imageURI = new Input_box("Change Profile Image",
                                         new TextBoxInformation("New Image URL",
                                                                "Paste new image url here"));
            imageURI.TextBoxes[0].Width += 300;
            imageURI.Width += 300;
            imageURI.ConfirmButton.Location = imageURI.ConfirmButton.Location with { X = imageURI.Width - 94 };
            imageURI.ClosedINBox += (_, args) => {
                try
                {
                    string[] validExtensions = { ".jpg", ".png", ".jpeg" };
                    Uri imageUri;
                    if (args.Inputs.Length == 1 && args.Inputs[0] == null) return;
                    if (!Uri.TryCreate(args.Inputs[0], UriKind.Absolute, out imageUri)) throw new Exception();
                    if (!validExtensions.Contains(Path.GetExtension(imageUri.AbsolutePath))) throw new Exception();

                    using var wc = new WebClient();
                    var imagePath = ControlsMisc.GetImageResourcesPath() + "\\" + UserName +
                                    Path.GetExtension(imageUri.AbsolutePath);
                    File.Create(imagePath).Close();
                    wc.DownloadFile(imageUri, imagePath);
                    var s = File.Open(imagePath, FileMode.Open);
                    var fromStream = Image.FromStream(s);
                    UserPicutreBox.BackgroundImage = OvalImage(fromStream);
                    s.Dispose();
                    ProfilePictureChanged(this,
                                          new ProfileImageChangedEventArgs(fromStream, UserName));
                    Pfp = fromStream;
                }
                catch (Exception ex)
                {
                    MessageBox.Show( "URL given does not correspond to an image file or the image extension is not supported", "Invalid URL"
                                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            Task.Run(() => Application.Run(imageURI));

        }

        private void ChangeImageByFileLbl_Click(object sender, EventArgs e)
        {
            string imageDir = null;
            var t = new Thread((ThreadStart)(() => {
                var op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "*Image Files (*.png, *.jpg, *.bmp, *.gif, *.jpeg)|*.png;*.jpg;*.bmp;*.gif;*.jpeg";
                if (op.ShowDialog() != DialogResult.OK) return;
                var pfpFileName = ControlsMisc.GetImageResourcesPath() + "\\" + UserName +
                                   Path.GetExtension(op.FileName);
                File.Copy(Path.GetFullPath(op.FileName), pfpFileName);
                imageDir = Path.GetFullPath(op.FileName);
                op.Dispose();
            }));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            if (imageDir == null) return;
            var s = File.Open(imageDir, FileMode.Open);
            var newProfileImage = Image.FromStream(s);
            UserPicutreBox.BackgroundImage = OvalImage(newProfileImage);
            s.Dispose();
            ProfilePictureChanged(this, new ProfileImageChangedEventArgs(newProfileImage, UserName));
            Pfp = newProfileImage;

        }

        private void DeleteProfilePictureLbl_Click(object sender, EventArgs e)
        {
            //File exists
            if (Pfp == null)
            {
                MessageBox.Show("You don't have a profile picture to delete", "Logic error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var confirmDeletion = MessageBox.Show("Are you sure you want to delete your current Profile Picture? " +
                                                  "\nNOTE: It will be gone forever and could not be retrieved" +
                                                  "\nIf you wish to proceed press YES, ", "CONFIRMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmDeletion != DialogResult.Yes) return;
            UserPicutreBox.BackgroundImage = OvalImage(DefaultUserProfileImage);
            ProfilePictureChanged(this, new ProfileImageChangedEventArgs(null, UserName));

        }
#nullable enable
        public static byte[]? ImageToByteArray(System.Drawing.Image? imageIn)
        {
            if (imageIn == null) return null;
            using var ms = new MemoryStream();
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }

        private void DeleteAccountLbl_Click(object sender, EventArgs e)
        {
            var confirmDeletion = MessageBox.Show("Are you sure you want to delete your Account? " +
                                                  "\nNOTE: It will be gone forever and could not be retrieved! If you're not" +
                                                  "sure than click 'NO'" +
                                                  "\nIf you wish to proceed click on YES, ", "CONFIRMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmDeletion != DialogResult.Yes) return;
            AccountDeleteAttempt(this, EventArgs.Empty);
        }

        private void UserPicutreBox_Click(object sender, EventArgs e)
        {
            ChangeImageByFileLbl_Click(sender, e);
        }
    }
}
