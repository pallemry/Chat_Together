﻿
namespace TestSocket
{
    sealed partial class SendData
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendData));
            this.sendButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.userIcons = new System.Windows.Forms.ImageList(this.components);
            this.messageInputTextBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeBgImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountStatsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeAdminPrivilegesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chatLog = new Form_Functions.ChatLog();
            this.adminToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suspenseUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.sendButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.sendButton.Location = new System.Drawing.Point(1761, 16);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(147, 84);
            this.sendButton.TabIndex = 1;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.closeButton.Location = new System.Drawing.Point(1608, 16);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(147, 84);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // userIcons
            // 
            this.userIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("userIcons.ImageStream")));
            this.userIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.userIcons.Images.SetKeyName(0, "google-forms-icon-free-and-vector-form-icon-11553444694xhldfmtngo-removebg-previe" +
        "w (3).png");
            this.userIcons.Images.SetKeyName(1, "Shopping_cart_icon.svg.ico");
            // 
            // messageInputTextBox
            // 
            this.messageInputTextBox.Font = new System.Drawing.Font("SimSun", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageInputTextBox.Location = new System.Drawing.Point(12, 16);
            this.messageInputTextBox.Name = "messageInputTextBox";
            this.messageInputTextBox.Size = new System.Drawing.Size(1590, 84);
            this.messageInputTextBox.TabIndex = 6;
            this.messageInputTextBox.Text = "";
            this.messageInputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.adminToolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1920, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeBgImageToolStripMenuItem,
            this.accountToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // changeBgImageToolStripMenuItem
            // 
            this.changeBgImageToolStripMenuItem.Name = "changeBgImageToolStripMenuItem";
            this.changeBgImageToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.changeBgImageToolStripMenuItem.Text = "Change Bg Image..";
            this.changeBgImageToolStripMenuItem.Click += new System.EventHandler(this.changeBgImageToolStripMenuItem_Click);
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountSettingsToolStripMenuItem,
            this.accountStatsToolStripMenuItem,
            this.changeAdminPrivilegesToolStripMenuItem});
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.accountToolStripMenuItem.Text = "Account";
            // 
            // accountSettingsToolStripMenuItem
            // 
            this.accountSettingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("accountSettingsToolStripMenuItem.Image")));
            this.accountSettingsToolStripMenuItem.Name = "accountSettingsToolStripMenuItem";
            this.accountSettingsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.accountSettingsToolStripMenuItem.Text = "Account Settings";
            this.accountSettingsToolStripMenuItem.Click += new System.EventHandler(this.accountSettingsToolStripMenuItem_Click);
            // 
            // accountStatsToolStripMenuItem
            // 
            this.accountStatsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("accountStatsToolStripMenuItem.Image")));
            this.accountStatsToolStripMenuItem.Name = "accountStatsToolStripMenuItem";
            this.accountStatsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.accountStatsToolStripMenuItem.Text = "Account Stats";
            this.accountStatsToolStripMenuItem.Click += new System.EventHandler(this.accountStatsToolStripMenuItem_Click);
            // 
            // changeAdminPrivilegesToolStripMenuItem
            // 
            this.changeAdminPrivilegesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("changeAdminPrivilegesToolStripMenuItem.Image")));
            this.changeAdminPrivilegesToolStripMenuItem.Name = "changeAdminPrivilegesToolStripMenuItem";
            this.changeAdminPrivilegesToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.changeAdminPrivilegesToolStripMenuItem.Text = "Acquire Admin Privileges";
            this.changeAdminPrivilegesToolStripMenuItem.Click += new System.EventHandler(this.changeAdminPrivilegesToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.sendButton);
            this.panel1.Controls.Add(this.messageInputTextBox);
            this.panel1.Controls.Add(this.closeButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 914);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 112);
            this.panel1.TabIndex = 8;
            // 
            // chatLog
            // 
            this.chatLog.AutoScroll = true;
            this.chatLog.BackColor = System.Drawing.Color.Teal;
            this.chatLog.BackgroundImage = global::TestSocket.Properties.Resources._3840x2160_lake_dark_night_starry_sky_landscape;
            this.chatLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.chatLog.DefaultGap = 20;
            this.chatLog.DefaultX = 30;
            this.chatLog.DefaultY = 30;
            this.chatLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.chatLog.Location = new System.Drawing.Point(0, 24);
            this.chatLog.Name = "chatLog";
            this.chatLog.Size = new System.Drawing.Size(1920, 890);
            this.chatLog.TabIndex = 5;
            this.chatLog.Load += new System.EventHandler(this.chatLog1_Load);
            // 
            // adminToolsToolStripMenuItem
            // 
            this.adminToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.suspenseUserToolStripMenuItem});
            this.adminToolsToolStripMenuItem.Name = "adminToolsToolStripMenuItem";
            this.adminToolsToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.adminToolsToolStripMenuItem.Text = "Admin Tools";
            // 
            // suspenseUserToolStripMenuItem
            // 
            this.suspenseUserToolStripMenuItem.Name = "suspenseUserToolStripMenuItem";
            this.suspenseUserToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.suspenseUserToolStripMenuItem.Text = "Suspense User";
            // 
            // SendData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1026);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chatLog);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SendData";
            this.Text = "SendData";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SendData_FormClosing);
            this.Load += new System.EventHandler(this.SendData_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ImageList userIcons;
        private Form_Functions.ChatLog chatLog;
        private System.Windows.Forms.RichTextBox messageInputTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeBgImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem accountStatsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeAdminPrivilegesToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem adminToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suspenseUserToolStripMenuItem;
    }
}