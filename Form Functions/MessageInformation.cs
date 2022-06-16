using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

using Form_Functions.src;

namespace Form_Functions
{
    public partial class MessageInformation : Form
    {
        public MessageInformation(MessageInformationArgs args)
        {
            
            InitializeComponent();
            BringToFront();
            FullMsgPanel.InitializePanelResize(this, FullMessageLbl);
            DateSentPanel.InitializePanelResize(this, DateSentLbl);
            MainMsgInfoPanel.InitializePanelResize(this, MainMsgInfoLbl);
            MainSndrInfoPanel.InitializePanelResize(this, MainSdrInfoLbl);
            SenderAdminPanel.InitializePanelResize(this, SenderAdminLbl);
            SenderUserNamePanel.InitializePanelResize(this, SenderUserName);
            SenderAgePanel.InitializePanelResize(this, SenderAgeLbl);
            FullMessageLbl.Text += " " + args.Message;
            DateSentLbl.Text += $"{args.DateSent:f}";

            SenderUserName.Text += " " + args.UserName;
            SenderAdminLbl.Text += args.IsUserAdmin ? " true" : " false";
            if (args.UserRegistrationDate != null)
            {
                SenderAgeLbl.Text += $@"{DateTime.Now.Subtract(args.UserRegistrationDate ?? throw new NoNullAllowedException()).Days} " +
                                     $@"Days, joined on {args.UserRegistrationDate:d}";
            }
            else
            {
                SenderAgeLbl.Text += $"Uknown";
            }
        }

        private void FullMessageLbl_SizeChanged(object sender, EventArgs e)
        {
        }

        private void DateSentLbl_SizeChanged(object sender, EventArgs e)
        {
            
        }
    }

}
