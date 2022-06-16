namespace Form_Functions
{
    partial class MessageInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageInformation));
            this.MainMsgInfoLbl = new System.Windows.Forms.Label();
            this.FullMessageLbl = new System.Windows.Forms.Label();
            this.DateSentLbl = new System.Windows.Forms.Label();
            this.MainSdrInfoLbl = new System.Windows.Forms.Label();
            this.SenderUserName = new System.Windows.Forms.Label();
            this.MainMsgInfoPanel = new System.Windows.Forms.Panel();
            this.FullMsgPanel = new System.Windows.Forms.Panel();
            this.DateSentPanel = new System.Windows.Forms.Panel();
            this.MainSndrInfoPanel = new System.Windows.Forms.Panel();
            this.SenderUserNamePanel = new System.Windows.Forms.Panel();
            this.SenderAdminPanel = new System.Windows.Forms.Panel();
            this.SenderAdminLbl = new System.Windows.Forms.Label();
            this.SenderAgeLbl = new System.Windows.Forms.Label();
            this.SenderAgePanel = new System.Windows.Forms.Panel();
            this.MainMsgInfoPanel.SuspendLayout();
            this.FullMsgPanel.SuspendLayout();
            this.DateSentPanel.SuspendLayout();
            this.MainSndrInfoPanel.SuspendLayout();
            this.SenderUserNamePanel.SuspendLayout();
            this.SenderAdminPanel.SuspendLayout();
            this.SenderAgePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMsgInfoLbl
            // 
            this.MainMsgInfoLbl.AutoSize = true;
            this.MainMsgInfoLbl.Font = new System.Drawing.Font("Garamond", 19F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMsgInfoLbl.Location = new System.Drawing.Point(39, 6);
            this.MainMsgInfoLbl.Name = "MainMsgInfoLbl";
            this.MainMsgInfoLbl.Size = new System.Drawing.Size(338, 29);
            this.MainMsgInfoLbl.TabIndex = 0;
            this.MainMsgInfoLbl.Text = "Message Related Information";
            // 
            // FullMessageLbl
            // 
            this.FullMessageLbl.AutoSize = true;
            this.FullMessageLbl.Font = new System.Drawing.Font("Garamond", 15.5F);
            this.FullMessageLbl.Location = new System.Drawing.Point(17, 2);
            this.FullMessageLbl.MaximumSize = new System.Drawing.Size(670, 0);
            this.FullMessageLbl.Name = "FullMessageLbl";
            this.FullMessageLbl.Size = new System.Drawing.Size(126, 24);
            this.FullMessageLbl.TabIndex = 1;
            this.FullMessageLbl.Text = "Full Message: ";
            this.FullMessageLbl.SizeChanged += new System.EventHandler(this.FullMessageLbl_SizeChanged);
            // 
            // DateSentLbl
            // 
            this.DateSentLbl.AutoSize = true;
            this.DateSentLbl.Font = new System.Drawing.Font("Garamond", 15.5F);
            this.DateSentLbl.Location = new System.Drawing.Point(17, 3);
            this.DateSentLbl.MaximumSize = new System.Drawing.Size(670, 0);
            this.DateSentLbl.Name = "DateSentLbl";
            this.DateSentLbl.Size = new System.Drawing.Size(96, 24);
            this.DateSentLbl.TabIndex = 2;
            this.DateSentLbl.Text = "Date Sent:";
            this.DateSentLbl.SizeChanged += new System.EventHandler(this.DateSentLbl_SizeChanged);
            // 
            // MainSdrInfoLbl
            // 
            this.MainSdrInfoLbl.AutoSize = true;
            this.MainSdrInfoLbl.Font = new System.Drawing.Font("Garamond", 19F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainSdrInfoLbl.Location = new System.Drawing.Point(39, 12);
            this.MainSdrInfoLbl.Name = "MainSdrInfoLbl";
            this.MainSdrInfoLbl.Size = new System.Drawing.Size(316, 29);
            this.MainSdrInfoLbl.TabIndex = 3;
            this.MainSdrInfoLbl.Text = "Sender Related Information";
            // 
            // SenderUserName
            // 
            this.SenderUserName.AutoSize = true;
            this.SenderUserName.Font = new System.Drawing.Font("Garamond", 15.5F);
            this.SenderUserName.Location = new System.Drawing.Point(17, 3);
            this.SenderUserName.MaximumSize = new System.Drawing.Size(670, 0);
            this.SenderUserName.Name = "SenderUserName";
            this.SenderUserName.Size = new System.Drawing.Size(171, 24);
            this.SenderUserName.TabIndex = 4;
            this.SenderUserName.Text = "Sender User Name:";
            // 
            // MainMsgInfoPanel
            // 
            this.MainMsgInfoPanel.Controls.Add(this.MainMsgInfoLbl);
            this.MainMsgInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainMsgInfoPanel.Location = new System.Drawing.Point(0, 0);
            this.MainMsgInfoPanel.Name = "MainMsgInfoPanel";
            this.MainMsgInfoPanel.Size = new System.Drawing.Size(692, 38);
            this.MainMsgInfoPanel.TabIndex = 6;
            // 
            // FullMsgPanel
            // 
            this.FullMsgPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FullMsgPanel.Controls.Add(this.FullMessageLbl);
            this.FullMsgPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FullMsgPanel.Location = new System.Drawing.Point(0, 38);
            this.FullMsgPanel.Name = "FullMsgPanel";
            this.FullMsgPanel.Size = new System.Drawing.Size(692, 27);
            this.FullMsgPanel.TabIndex = 7;
            // 
            // DateSentPanel
            // 
            this.DateSentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DateSentPanel.Controls.Add(this.DateSentLbl);
            this.DateSentPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.DateSentPanel.Location = new System.Drawing.Point(0, 65);
            this.DateSentPanel.Name = "DateSentPanel";
            this.DateSentPanel.Size = new System.Drawing.Size(692, 27);
            this.DateSentPanel.TabIndex = 8;
            // 
            // MainSndrInfoPanel
            // 
            this.MainSndrInfoPanel.Controls.Add(this.MainSdrInfoLbl);
            this.MainSndrInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MainSndrInfoPanel.Location = new System.Drawing.Point(0, 92);
            this.MainSndrInfoPanel.Name = "MainSndrInfoPanel";
            this.MainSndrInfoPanel.Size = new System.Drawing.Size(692, 44);
            this.MainSndrInfoPanel.TabIndex = 9;
            // 
            // SenderUserNamePanel
            // 
            this.SenderUserNamePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SenderUserNamePanel.Controls.Add(this.SenderUserName);
            this.SenderUserNamePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SenderUserNamePanel.Location = new System.Drawing.Point(0, 136);
            this.SenderUserNamePanel.Name = "SenderUserNamePanel";
            this.SenderUserNamePanel.Size = new System.Drawing.Size(692, 31);
            this.SenderUserNamePanel.TabIndex = 10;
            // 
            // SenderAdminPanel
            // 
            this.SenderAdminPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SenderAdminPanel.Controls.Add(this.SenderAdminLbl);
            this.SenderAdminPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SenderAdminPanel.Location = new System.Drawing.Point(0, 167);
            this.SenderAdminPanel.Name = "SenderAdminPanel";
            this.SenderAdminPanel.Size = new System.Drawing.Size(692, 31);
            this.SenderAdminPanel.TabIndex = 5;
            // 
            // SenderAdminLbl
            // 
            this.SenderAdminLbl.AutoSize = true;
            this.SenderAdminLbl.Font = new System.Drawing.Font("Garamond", 15.5F);
            this.SenderAdminLbl.Location = new System.Drawing.Point(17, 2);
            this.SenderAdminLbl.MaximumSize = new System.Drawing.Size(670, 0);
            this.SenderAdminLbl.Name = "SenderAdminLbl";
            this.SenderAdminLbl.Size = new System.Drawing.Size(228, 24);
            this.SenderAdminLbl.TabIndex = 5;
            this.SenderAdminLbl.Text = "Sender Admin Privilleges: ";
            // 
            // SenderAgeLbl
            // 
            this.SenderAgeLbl.AutoSize = true;
            this.SenderAgeLbl.Font = new System.Drawing.Font("Garamond", 15.5F);
            this.SenderAgeLbl.Location = new System.Drawing.Point(18, 3);
            this.SenderAgeLbl.MaximumSize = new System.Drawing.Size(670, 0);
            this.SenderAgeLbl.Name = "SenderAgeLbl";
            this.SenderAgeLbl.Size = new System.Drawing.Size(184, 24);
            this.SenderAgeLbl.TabIndex = 6;
            this.SenderAgeLbl.Text = "Sender Account Age:";
            // 
            // SenderAgePanel
            // 
            this.SenderAgePanel.Controls.Add(this.SenderAgeLbl);
            this.SenderAgePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SenderAgePanel.Location = new System.Drawing.Point(0, 198);
            this.SenderAgePanel.Name = "SenderAgePanel";
            this.SenderAgePanel.Size = new System.Drawing.Size(692, 31);
            this.SenderAgePanel.TabIndex = 11;
            // 
            // MessageInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 233);
            this.Controls.Add(this.SenderAgePanel);
            this.Controls.Add(this.SenderAdminPanel);
            this.Controls.Add(this.SenderUserNamePanel);
            this.Controls.Add(this.MainSndrInfoPanel);
            this.Controls.Add(this.DateSentPanel);
            this.Controls.Add(this.FullMsgPanel);
            this.Controls.Add(this.MainMsgInfoPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MessageInformation";
            this.Text = "MessageInformation";
            this.MainMsgInfoPanel.ResumeLayout(false);
            this.MainMsgInfoPanel.PerformLayout();
            this.FullMsgPanel.ResumeLayout(false);
            this.FullMsgPanel.PerformLayout();
            this.DateSentPanel.ResumeLayout(false);
            this.DateSentPanel.PerformLayout();
            this.MainSndrInfoPanel.ResumeLayout(false);
            this.MainSndrInfoPanel.PerformLayout();
            this.SenderUserNamePanel.ResumeLayout(false);
            this.SenderUserNamePanel.PerformLayout();
            this.SenderAdminPanel.ResumeLayout(false);
            this.SenderAdminPanel.PerformLayout();
            this.SenderAgePanel.ResumeLayout(false);
            this.SenderAgePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label MainMsgInfoLbl;
        private System.Windows.Forms.Label FullMessageLbl;
        private System.Windows.Forms.Label DateSentLbl;
        private System.Windows.Forms.Label MainSdrInfoLbl;
        private System.Windows.Forms.Label SenderUserName;
        private System.Windows.Forms.Panel MainMsgInfoPanel;
        private System.Windows.Forms.Panel FullMsgPanel;
        private System.Windows.Forms.Panel DateSentPanel;
        private System.Windows.Forms.Panel MainSndrInfoPanel;
        private System.Windows.Forms.Panel SenderUserNamePanel;
        private System.Windows.Forms.Panel SenderAdminPanel;
        private System.Windows.Forms.Label SenderAdminLbl;
        private System.Windows.Forms.Label SenderAgeLbl;
        private System.Windows.Forms.Panel SenderAgePanel;
    }
}