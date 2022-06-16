
namespace Form_Functions
{
    partial class LogIn
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
            this.UserName = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.Confirm = new System.Windows.Forms.Button();
            this.logInGb = new System.Windows.Forms.GroupBox();
            this.ShowPasswordBTN = new System.Windows.Forms.CheckBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SignUpGb = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Cancel2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.UserNameSU = new System.Windows.Forms.TextBox();
            this.PasswordSU = new System.Windows.Forms.TextBox();
            this.SingUpButton = new System.Windows.Forms.Button();
            this.logInGb.SuspendLayout();
            this.SignUpGb.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(15, 19);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(163, 20);
            this.UserName.TabIndex = 0;
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(15, 45);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(163, 20);
            this.Password.TabIndex = 1;
            this.Password.UseSystemPasswordChar = true;
            // 
            // Confirm
            // 
            this.Confirm.Location = new System.Drawing.Point(103, 71);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(75, 23);
            this.Confirm.TabIndex = 2;
            this.Confirm.Text = "Confirm";
            this.Confirm.UseVisualStyleBackColor = true;
            this.Confirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // logInGb
            // 
            this.logInGb.Controls.Add(this.ShowPasswordBTN);
            this.logInGb.Controls.Add(this.Cancel);
            this.logInGb.Controls.Add(this.label1);
            this.logInGb.Controls.Add(this.UserName);
            this.logInGb.Controls.Add(this.Password);
            this.logInGb.Controls.Add(this.Confirm);
            this.logInGb.Location = new System.Drawing.Point(12, 12);
            this.logInGb.Name = "logInGb";
            this.logInGb.Size = new System.Drawing.Size(200, 161);
            this.logInGb.TabIndex = 4;
            this.logInGb.TabStop = false;
            this.logInGb.Text = "Log In";
            // 
            // ShowPasswordBTN
            // 
            this.ShowPasswordBTN.AutoSize = true;
            this.ShowPasswordBTN.Font = new System.Drawing.Font("Miriam Fixed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowPasswordBTN.Location = new System.Drawing.Point(25, 100);
            this.ShowPasswordBTN.Name = "ShowPasswordBTN";
            this.ShowPasswordBTN.Size = new System.Drawing.Size(143, 19);
            this.ShowPasswordBTN.TabIndex = 5;
            this.ShowPasswordBTN.Text = "Show Password";
            this.ShowPasswordBTN.UseVisualStyleBackColor = true;
            this.ShowPasswordBTN.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(15, 71);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(82, 23);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Miriam Fixed", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-3, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 33);
            this.label1.TabIndex = 3;
            this.label1.Text = "Don\'t have an account? \r\nSign up for free!";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // SignUpGb
            // 
            this.SignUpGb.Controls.Add(this.checkBox1);
            this.SignUpGb.Controls.Add(this.Cancel2);
            this.SignUpGb.Controls.Add(this.label2);
            this.SignUpGb.Controls.Add(this.UserNameSU);
            this.SignUpGb.Controls.Add(this.PasswordSU);
            this.SignUpGb.Controls.Add(this.SingUpButton);
            this.SignUpGb.Location = new System.Drawing.Point(241, 15);
            this.SignUpGb.Name = "SignUpGb";
            this.SignUpGb.Size = new System.Drawing.Size(200, 161);
            this.SignUpGb.TabIndex = 6;
            this.SignUpGb.TabStop = false;
            this.SignUpGb.Text = "Sign Up";
            this.SignUpGb.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Miriam Fixed", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(25, 100);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(143, 19);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Show Password";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Cancel2
            // 
            this.Cancel2.Location = new System.Drawing.Point(15, 71);
            this.Cancel2.Name = "Cancel2";
            this.Cancel2.Size = new System.Drawing.Size(82, 23);
            this.Cancel2.TabIndex = 4;
            this.Cancel2.Text = "Cancel";
            this.Cancel2.UseVisualStyleBackColor = true;
            this.Cancel2.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Miriam Fixed", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(-3, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "Already a member?\r\nLog In here!";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // UserNameSU
            // 
            this.UserNameSU.Location = new System.Drawing.Point(15, 19);
            this.UserNameSU.Name = "UserNameSU";
            this.UserNameSU.Size = new System.Drawing.Size(163, 20);
            this.UserNameSU.TabIndex = 0;
            // 
            // PasswordSU
            // 
            this.PasswordSU.Location = new System.Drawing.Point(15, 45);
            this.PasswordSU.Name = "PasswordSU";
            this.PasswordSU.Size = new System.Drawing.Size(163, 20);
            this.PasswordSU.TabIndex = 1;
            this.PasswordSU.UseSystemPasswordChar = true;
            // 
            // SingUpButton
            // 
            this.SingUpButton.Location = new System.Drawing.Point(103, 71);
            this.SingUpButton.Name = "SingUpButton";
            this.SingUpButton.Size = new System.Drawing.Size(75, 23);
            this.SingUpButton.TabIndex = 2;
            this.SingUpButton.Text = "Sign-Up";
            this.SingUpButton.UseVisualStyleBackColor = true;
            this.SingUpButton.Click += new System.EventHandler(this.SingUpButton_Click);
            // 
            // LogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 201);
            this.Controls.Add(this.SignUpGb);
            this.Controls.Add(this.logInGb);
            this.Name = "LogIn";
            this.Text = "Log In";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LogIn_FormClosed);
            this.Load += new System.EventHandler(this.LogIn_Load);
            this.logInGb.ResumeLayout(false);
            this.logInGb.PerformLayout();
            this.SignUpGb.ResumeLayout(false);
            this.SignUpGb.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button Confirm;
        private System.Windows.Forms.GroupBox logInGb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.CheckBox ShowPasswordBTN;
        private System.Windows.Forms.GroupBox SignUpGb;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button Cancel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UserNameSU;
        private System.Windows.Forms.TextBox PasswordSU;
        private System.Windows.Forms.Button SingUpButton;
    }
}