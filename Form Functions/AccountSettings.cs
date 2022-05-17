﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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

        public AccountSettings(string userName, string password, Image originalPfp)
        {
            InitializeComponent();
            UserName = userName;
            Password = password;
            PasswordLabel.Text = "";
            UserNameLabel.Text = userName;
            EditPasswordLabel.Text = @"Edit..";
            EditPasswordLabel.ForeColor = Color.Gray;
            EditPasswordLabel.InitializeLinkLabel(FontStyle.Underline, EditPasswordLabel.Font.Size, Color.RoyalBlue);
            ChangeImageButtonLabel.InitializeLinkLabel(FontStyle.Underline, EditPasswordLabel.Font.Size, Color.RoyalBlue);
            ChangeImageByFileLbl.InitializeLinkLabel(FontStyle.Underline, EditPasswordLabel.Font.Size, Color.RoyalBlue);
            DeleteProfilePictureLbl.InitializeLinkLabel(FontStyle.Underline, EditPasswordLabel.Font.Size, Color.RoyalBlue);
            UserPicutreBox.BackgroundImage = OvalImage(originalPfp);
            for (int i = 0; i < password.Length; i++)
            {
                PasswordLabel.Text += "*";
            }


        }
        public static Image GetProfilePictureByUserName(string userName)
        {
            Image pfp = null;
            var dir = new DirectoryInfo(ControlsMisc.GetResourcesPath());
            string[] validExtensions = { ".jpg", ".png", ".jpeg" }; 
            FileInfo[] files = dir.GetFiles(userName + ".*");
            if (files.Length > 0)
            {
                foreach (FileInfo file in files.Where(info => validExtensions.Contains(info.Extension)))
                {
                    try
                    {
                        var s = file.Open(FileMode.Open);
                        pfp = Image.FromStream(s);
                        s.Dispose();

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Failed To Load Profile Picture!", "Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        pfp = Image.FromFile(ControlsMisc.GetResourcesPath() + "\\def\\defaultUser.png");
                    }
                    

                }
            }
            else
            {
                pfp = Image.FromFile(ControlsMisc.GetResourcesPath() + "\\def\\defaultUser.png");
            }

            return pfp;
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
            imageURI.TextBoxes[0].Width += 300;
            imageURI.Width += 300;
            imageURI.ConfirmButton.Location = imageURI.ConfirmButton.Location with { X = imageURI.Width - 94 };
            imageURI.ClosedINBox += (o, args) => {
                try
                {
                    string[] validExtensions = { ".jpg", ".png", ".jpeg" };
                    Uri imageUri;

                    if (!Uri.TryCreate(args.Inputs[0], UriKind.Absolute, out imageUri)) throw new Exception();
                    if (!validExtensions.Contains(Path.GetExtension(imageUri.AbsolutePath))) throw new Exception();

                    using var wc = new WebClient();
                    var imagePath = ControlsMisc.GetResourcesPath() + "\\" + UserName +
                                    Path.GetExtension(imageUri.AbsolutePath);
                    var dir = new DirectoryInfo(ControlsMisc.GetResourcesPath());
                    FileInfo[] files = dir.GetFiles(UserName + ".*");
                    
                    if (files.Length > 0)
                    {
                        //File exists
                        foreach (FileInfo file in files)
                        {
                            
                            File.Delete(file.FullName);
                            break;
                        }
                    }
                    else
                    {
                        File.Create(imagePath).Close();
                    }
                    wc.DownloadFile(imageUri, imagePath);
                    var s = File.Open(imagePath, FileMode.Open);
                    UserPicutreBox.BackgroundImage = OvalImage(Image.FromStream(s));
                    s.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Source + "\n" + ex.Message + "\n" + ex.StackTrace);
                    
                    return;
                }
            };
            Task.Run(() => Application.Run(imageURI));

        }

        private void ChangeImageByFileLbl_Click(object sender, EventArgs e)
        {
            string imageDir = null;
            Thread t = new Thread((ThreadStart)(() => {
                OpenFileDialog op = new OpenFileDialog();
                op.Multiselect = false;
                op.Filter = "*Image Files (*.png, *.jpg, *.bmp, *.gif, *.jpeg)|*.png;*.jpg;*.bmp;*.gif;*.jpeg";
                if (op.ShowDialog() != DialogResult.OK) return;
                
                var dir = new DirectoryInfo(ControlsMisc.GetResourcesPath());
                FileInfo[] files = dir.GetFiles(UserName + ".*");
                if (files.Length > 0)
                {
                    //File exists
                    foreach (var file in files)
                    {
                        file.Delete();
                        
                    }

                }

                File.Copy(Path.GetFullPath(op.FileName), ControlsMisc.GetResourcesPath() + "\\" + UserName +
                                                         Path.GetExtension(op.FileName));
                
                op.Dispose();
            }));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();

            UserPicutreBox.BackgroundImage = OvalImage(GetProfilePictureByUserName(UserName));
            
        }

        private void DeleteProfilePictureLbl_Click(object sender, EventArgs e)
        {
            var dir = new DirectoryInfo(ControlsMisc.GetResourcesPath());
            FileInfo[] files = dir.GetFiles(UserName + ".*");

            if (files.Length > 0)
            {
                //File exists
                var confirmDeletion = MessageBox.Show("Are you sure you want to delete your current Profile Picture? " +
                                                      "\nNOTE: It will be gone forever and could not be retrieved" +
                                                      "\nIf you wish to proceed press YES, ", "CONFIRMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmDeletion != DialogResult.Yes) return;
                foreach (var file in files)
                {
                    File.Delete(file.FullName);
                }
                UserPicutreBox.BackgroundImage = OvalImage(Image.FromFile(dir.FullName + "\\def\\defaultUser.png"));
                
            }
            else
            {
                MessageBox.Show("You don't have a profile picture to delete", "Logic error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
    }
}
