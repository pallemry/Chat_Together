
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
            this.ChangeImageButtonLabel.Location = new System.Drawing.Point(221, 241);
            this.ChangeImageButtonLabel.Name = "ChangeImageButtonLabel";
            this.ChangeImageButtonLabel.Size = new System.Drawing.Size(34, 19);
            this.ChangeImageButtonLabel.TabIndex = 6;
            this.ChangeImageButtonLabel.Text = "test";
            this.ChangeImageButtonLabel.Click += new System.EventHandler(this.ChangeImageButtonLabel_Click);
            // 
            // AccountSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 450);
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
    }
}