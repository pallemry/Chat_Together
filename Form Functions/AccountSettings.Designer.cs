﻿
namespace Form_Functions
{
    partial class AccountSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.UserPicutreBox = new System.Windows.Forms.PictureBox();
            this.PasswordTitleLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.EditPasswordLabel = new System.Windows.Forms.Label();
            this.ChangeImageLabel = new System.Windows.Forms.Label();
            this.ChangeImageButtonLabel = new System.Windows.Forms.Label();
            this.ChangeImageByFileLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DeleteProfilePictureLbl = new System.Windows.Forms.Label();
            this.DeleteAccountLbl = new System.Windows.Forms.Label();
            this.EmailAddressTitle = new System.Windows.Forms.Label();
            this.EmailAddressLabel = new System.Windows.Forms.Label();
            this.EditEmailAddressLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UserPicutreBox)).BeginInit();
            this.SuspendLayout();
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Font = new System.Drawing.Font("Inter SemiBold", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameLabel.Location = new System.Drawing.Point(218, 87);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(116, 39);
            this.UserNameLabel.TabIndex = 0;
            this.UserNameLabel.Text = "Admin";
            // 
            // UserPicutreBox
            // 
            this.UserPicutreBox.BackgroundImage = global::Form_Functions.Properties.Resources._3840x2160_lake_dark_night_starry_sky_landscape;
            this.UserPicutreBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.UserPicutreBox.Location = new System.Drawing.Point(12, 12);
            this.UserPicutreBox.Name = "UserPicutreBox";
            this.UserPicutreBox.Size = new System.Drawing.Size(200, 200);
            this.UserPicutreBox.TabIndex = 1;
            this.UserPicutreBox.TabStop = false;
            this.UserPicutreBox.Click += new System.EventHandler(this.UserPicutreBox_Click);
            // 
            // PasswordTitleLabel
            // 
            this.PasswordTitleLabel.AutoSize = true;
            this.PasswordTitleLabel.Font = new System.Drawing.Font("Inter SemiBold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordTitleLabel.Location = new System.Drawing.Point(12, 215);
            this.PasswordTitleLabel.Name = "PasswordTitleLabel";
            this.PasswordTitleLabel.Size = new System.Drawing.Size(117, 26);
            this.PasswordTitleLabel.TabIndex = 2;
            this.PasswordTitleLabel.Text = "Password";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Inter SemiBold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLabel.Location = new System.Drawing.Point(12, 241);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(45, 23);
            this.PasswordLabel.TabIndex = 3;
            this.PasswordLabel.Text = "test";
            // 
            // EditPasswordLabel
            // 
            this.EditPasswordLabel.AutoSize = true;
            this.EditPasswordLabel.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditPasswordLabel.Location = new System.Drawing.Point(13, 264);
            this.EditPasswordLabel.Name = "EditPasswordLabel";
            this.EditPasswordLabel.Size = new System.Drawing.Size(34, 19);
            this.EditPasswordLabel.TabIndex = 4;
            this.EditPasswordLabel.Text = "test";
            this.EditPasswordLabel.Click += new System.EventHandler(this.EditPasswordLabel_Click);
            // 
            // ChangeImageLabel
            // 
            this.ChangeImageLabel.AutoSize = true;
            this.ChangeImageLabel.Font = new System.Drawing.Font("Inter SemiBold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeImageLabel.Location = new System.Drawing.Point(220, 215);
            this.ChangeImageLabel.Name = "ChangeImageLabel";
            this.ChangeImageLabel.Size = new System.Drawing.Size(165, 26);
            this.ChangeImageLabel.TabIndex = 5;
            this.ChangeImageLabel.Text = "Change Image";
            // 
            // ChangeImageButtonLabel
            // 
            this.ChangeImageButtonLabel.AutoSize = true;
            this.ChangeImageButtonLabel.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeImageButtonLabel.Location = new System.Drawing.Point(321, 264);
            this.ChangeImageButtonLabel.Name = "ChangeImageButtonLabel";
            this.ChangeImageButtonLabel.Size = new System.Drawing.Size(85, 19);
            this.ChangeImageButtonLabel.TabIndex = 6;
            this.ChangeImageButtonLabel.Text = "URL image";
            this.ChangeImageButtonLabel.Click += new System.EventHandler(this.ChangeImageButtonLabel_Click);
            // 
            // ChangeImageByFileLbl
            // 
            this.ChangeImageByFileLbl.AutoSize = true;
            this.ChangeImageByFileLbl.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeImageByFileLbl.Location = new System.Drawing.Point(221, 264);
            this.ChangeImageByFileLbl.Name = "ChangeImageByFileLbl";
            this.ChangeImageByFileLbl.Size = new System.Drawing.Size(94, 19);
            this.ChangeImageByFileLbl.TabIndex = 7;
            this.ChangeImageByFileLbl.Text = "Exisiting File";
            this.ChangeImageByFileLbl.Click += new System.EventHandler(this.ChangeImageByFileLbl_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gadugi", 14F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(221, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "Change  Profile picture by..";
            // 
            // DeleteProfilePictureLbl
            // 
            this.DeleteProfilePictureLbl.AutoSize = true;
            this.DeleteProfilePictureLbl.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteProfilePictureLbl.Location = new System.Drawing.Point(450, 264);
            this.DeleteProfilePictureLbl.Name = "DeleteProfilePictureLbl";
            this.DeleteProfilePictureLbl.Size = new System.Drawing.Size(154, 19);
            this.DeleteProfilePictureLbl.TabIndex = 9;
            this.DeleteProfilePictureLbl.Text = "Delete Profile Picture";
            this.DeleteProfilePictureLbl.Click += new System.EventHandler(this.DeleteProfilePictureLbl_Click);
            // 
            // DeleteAccountLbl
            // 
            this.DeleteAccountLbl.AutoSize = true;
            this.DeleteAccountLbl.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteAccountLbl.Location = new System.Drawing.Point(480, 422);
            this.DeleteAccountLbl.Name = "DeleteAccountLbl";
            this.DeleteAccountLbl.Size = new System.Drawing.Size(113, 19);
            this.DeleteAccountLbl.TabIndex = 10;
            this.DeleteAccountLbl.Text = "Delete Account";
            this.DeleteAccountLbl.Click += new System.EventHandler(this.DeleteAccountLbl_Click);
            // 
            // EmailAddressTitle
            // 
            this.EmailAddressTitle.AutoSize = true;
            this.EmailAddressTitle.Font = new System.Drawing.Font("Inter SemiBold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailAddressTitle.Location = new System.Drawing.Point(12, 305);
            this.EmailAddressTitle.Name = "EmailAddressTitle";
            this.EmailAddressTitle.Size = new System.Drawing.Size(176, 26);
            this.EmailAddressTitle.TabIndex = 11;
            this.EmailAddressTitle.Text = "Recovery Email";
            // 
            // EmailAddressLabel
            // 
            this.EmailAddressLabel.AutoSize = true;
            this.EmailAddressLabel.Font = new System.Drawing.Font("Inter SemiBold", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailAddressLabel.Location = new System.Drawing.Point(13, 331);
            this.EmailAddressLabel.Name = "EmailAddressLabel";
            this.EmailAddressLabel.Size = new System.Drawing.Size(45, 23);
            this.EmailAddressLabel.TabIndex = 12;
            this.EmailAddressLabel.Text = "test";
            // 
            // EditEmailAddressLabel
            // 
            this.EditEmailAddressLabel.AutoSize = true;
            this.EditEmailAddressLabel.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditEmailAddressLabel.Location = new System.Drawing.Point(13, 354);
            this.EditEmailAddressLabel.Name = "EditEmailAddressLabel";
            this.EditEmailAddressLabel.Size = new System.Drawing.Size(34, 19);
            this.EditEmailAddressLabel.TabIndex = 13;
            this.EditEmailAddressLabel.Text = "test";
            this.EditEmailAddressLabel.Click += new System.EventHandler(this.EditEmailAddressLabel_Click);
            // 
            // AccountSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 450);
            this.Controls.Add(this.EditEmailAddressLabel);
            this.Controls.Add(this.EmailAddressLabel);
            this.Controls.Add(this.EmailAddressTitle);
            this.Controls.Add(this.DeleteAccountLbl);
            this.Controls.Add(this.DeleteProfilePictureLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChangeImageByFileLbl);
            this.Controls.Add(this.ChangeImageButtonLabel);
            this.Controls.Add(this.ChangeImageLabel);
            this.Controls.Add(this.EditPasswordLabel);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.PasswordTitleLabel);
            this.Controls.Add(this.UserPicutreBox);
            this.Controls.Add(this.UserNameLabel);
            this.Name = "AccountSettings";
            this.Text = "AccountSettings";
            ((System.ComponentModel.ISupportInitialize)(this.UserPicutreBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.PictureBox UserPicutreBox;
        private System.Windows.Forms.Label PasswordTitleLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label EditPasswordLabel;
        private System.Windows.Forms.Label ChangeImageLabel;
        private System.Windows.Forms.Label ChangeImageButtonLabel;
        private System.Windows.Forms.Label ChangeImageByFileLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DeleteProfilePictureLbl;
        private System.Windows.Forms.Label DeleteAccountLbl;
        private System.Windows.Forms.Label EmailAddressTitle;
        private System.Windows.Forms.Label EmailAddressLabel;
        private System.Windows.Forms.Label EditEmailAddressLabel;
    }
}