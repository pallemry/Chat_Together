
using System;

namespace Chat_Together
{
    partial class ChatMessage
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


            while (true)
            {
                try { base.Dispose(disposing); break; }
                catch (Exception e)
                { }
            }
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Author = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.InformationButton = new System.Windows.Forms.PictureBox();
            this.UserImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.InformationButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserImage)).BeginInit();
            this.SuspendLayout();
            // 
            // Author
            // 
            this.Author.AutoSize = true;
            this.Author.Font = new System.Drawing.Font("MS PGothic", 12.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.Author.Location = new System.Drawing.Point(67, 12);
            this.Author.Name = "Author";
            this.Author.Size = new System.Drawing.Size(56, 17);
            this.Author.TabIndex = 0;
            this.Author.Text = "label1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.richTextBox1.Location = new System.Drawing.Point(67, 38);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(217, 23);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            this.richTextBox1.MouseEnter += new System.EventHandler(this.ChatMessage_MouseEnter);
            this.richTextBox1.MouseLeave += new System.EventHandler(this.ChatMessage_MouseLeave);
            // 
            // InformationButton
            // 
            this.InformationButton.BackColor = System.Drawing.Color.Transparent;
            this.InformationButton.BackgroundImage = global::Form_Functions.Properties.Resources.Dots_vertical_more_setting_user_interface_web_app_512;
            this.InformationButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.InformationButton.Location = new System.Drawing.Point(322, 3);
            this.InformationButton.Name = "InformationButton";
            this.InformationButton.Size = new System.Drawing.Size(27, 26);
            this.InformationButton.TabIndex = 4;
            this.InformationButton.TabStop = false;
            this.InformationButton.Visible = false;
            this.InformationButton.Click += new System.EventHandler(this.InformationButton_Click);
            // 
            // UserImage
            // 
            this.UserImage.Location = new System.Drawing.Point(12, 12);
            this.UserImage.Name = "UserImage";
            this.UserImage.Size = new System.Drawing.Size(49, 41);
            this.UserImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UserImage.TabIndex = 1;
            this.UserImage.TabStop = false;
            // 
            // ChatMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.InformationButton);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.UserImage);
            this.Controls.Add(this.Author);
            this.Name = "ChatMessage";
            this.Size = new System.Drawing.Size(352, 92);
            this.Load += new System.EventHandler(this.ChatMessage_Load);
            this.MouseEnter += new System.EventHandler(this.ChatMessage_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ChatMessage_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.InformationButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label Author;
        public System.Windows.Forms.PictureBox UserImage;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox InformationButton;
    }
}
