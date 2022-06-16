using System;
using System.Drawing;
using System.Windows.Forms;
#nullable enable
namespace Form_Functions
{
    public partial class LogIn : Form
    {
        public event LoggedInEventHandler LoggedIn;
        public bool Running { get; private set; }
        public LogIn()
        {
            InitializeComponent();
            CenterToScreen();
            SignUpGb.Location = logInGb.Location;
            Running = true;

        }
        private void LogIn_Load(object sender, EventArgs e)
        {
            
            label1.InitializeLinkLabel(FontStyle.Underline , label1.Font.Size, Color.RoyalBlue);
            label2.InitializeLinkLabel(FontStyle.Underline , label2.Font.Size, Color.RoyalBlue);
            Password.InitializePlaceHolder("Enter Password..",
                                           Password.ForeColor,
                                           Password.BackColor,
                                           Color.Gray,
                                           Password.BackColor);
            UserName.InitializePlaceHolder("Enter User Name..",
                                           UserName.ForeColor,
                                           UserName.BackColor, 
                                           Color.Gray,
                                           UserName.BackColor);
            PasswordSU.InitializePlaceHolder("Enter Password..",
                                           PasswordSU.ForeColor,
                                           PasswordSU.BackColor,
                                           Color.Gray,
                                           PasswordSU.BackColor);
            UserNameSU.InitializePlaceHolder("Enter User Name..",
                                           UserNameSU.ForeColor,
                                           UserNameSU.BackColor,
                                           Color.Gray,
                                           UserNameSU.BackColor);
            ControlsMisc.LinkPasswordWithCheckBox(PasswordSU, checkBox1);
        }

        private void LogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Running = false;
            CloseFormWithData(new LoggedInEventArgs(UserName.Text, Password.Text, Closer.User, false));
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            if (Password.BackColor == Color.Gray)
            {
                MessageBox.Show("Must fill password");
                return;
            }
            CloseFormWithData(new LoggedInEventArgs(UserName.Text, Password.Text, Closer.ConfirmButton, false));
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            CloseFormWithData(new LoggedInEventArgs(UserName.Text, Password.Text, Closer.CancelButton, false));
        }

        private void CloseFormWithData(LoggedInEventArgs args)
        {
            if (args.Closer != Closer.User)
            {
                Close();
            }
            LoggedIn?.Invoke(this, args);
        }
        public class LoggedInEventArgs
        {
            public LoggedInEventArgs(string? userName, string? password, Closer closer, bool newAccount)
            {
                UserName = userName;
                Closer = closer;
                Password = password;
                NewAccount = newAccount;
            }
            public Closer Closer { get; private set; }
            public string? UserName { get; private set; }
            public string? Password { get; private set; }
            public bool NewAccount{ get; private set; }
        }

        public delegate void LoggedInEventHandler(LogIn owner, LoggedInEventArgs args);

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Password.UseSystemPasswordChar = !ShowPasswordBTN.Checked;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Text = logInGb.Visible ? "Sign Up" : "Log In";
            logInGb.Visible = !logInGb.Visible;
            SignUpGb.Visible = !SignUpGb.Visible;
        }

        private void SingUpButton_Click(object sender, EventArgs e)
        {
            if (PasswordSU.BackColor == Color.Gray)
            {
                MessageBox.Show("Must fill password");
                return;
            }
            CloseFormWithData(new LoggedInEventArgs(UserNameSU.Text, PasswordSU.Text, Closer.ConfirmButton, true));
        }
    }
}
