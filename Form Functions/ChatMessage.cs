using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
#nullable enable
namespace Form_Functions
{
    public partial class ChatMessage : UserControl
    {
        public string Message { get; }
        public int? ID { get; set; }
        public event EventHandler? InformationClicked;
        private readonly Stopwatch _mouseLeaveStopwatch;
        public bool IsSystemMessage { get; }
        
        public ChatMessage(string message, float width, string? author, bool isSystemMessage = false)
        {
            _mouseLeaveStopwatch = new Stopwatch();

            IsSystemMessage = isSystemMessage;
            Message = message;

            InitializeComponent();

            #region Resize message calculator

            Width = (int)(width + 67);
            messageTextBox.Text = message;
            messageTextBox.Width = (int)width - 15;
            messageTextBox.ReadOnly = true;
            messageTextBox.AutoSize = false;
            foreach (var unused in messageTextBox.Lines)
            {
                if (unused is not null && !string.IsNullOrEmpty(unused.Trim())) continue;
                if (messageTextBox.Lines.Length <= 0) continue;
                var lines = messageTextBox.Lines.ToList();
                lines.RemoveAt(lines.Count - 1);
                messageTextBox.Lines = lines.ToArray();
            }
            var height = (int)CreateGraphics().MeasureString(messageTextBox.Text, messageTextBox.Font, new SizeF(width, 99999)
                                                             , StringFormat.GenericDefault).Height;
            messageTextBox.Height = (int)(height * 1.2);

            var fixedMsgHs = messageTextBox.Height + 38;
            var fixedImgHs = UserImage.Location.Y + UserImage.Height + 20;
            Height = fixedMsgHs <= fixedImgHs ? fixedImgHs : fixedMsgHs + 20;
            var w = (int)CreateGraphics().MeasureString(messageTextBox.Text, messageTextBox.Font, new SizeF(width, 99999)
                                                        , StringFormat.GenericDefault).Width + 20;
            var nameW = (int)CreateGraphics().MeasureString(author, Author.Font, new SizeF(9999, 99999)
                                                            , StringFormat.GenericDefault).Width + 20;

            if (w < width)
            {
                messageTextBox.Width = w;
            }
            if (nameW > w)
                Width = nameW + 67;
            else
                Width = w + 67;

            #endregion

            InformationButton.Location = InformationButton.Location with { X = Width - InformationButton.Width - 10 };
        }

        private void ChatMessage_Load(object sender, EventArgs e)
        {
  ;
        }

        private void ChatMessage_MouseEnter(object sender, EventArgs e)
        {
            if (IsSystemMessage) return;
            InformationButton.Visible = true;
            _mouseLeaveStopwatch.Start();
        }

        private void ChatMessage_MouseLeave(object sender, EventArgs e)
        {
            if (_mouseLeaveStopwatch.Elapsed.TotalMilliseconds < 100) return;
            InformationButton.Visible = false;
            _mouseLeaveStopwatch.Stop(); 
            _mouseLeaveStopwatch.Reset();
        }

        private void InformationButton_Click(object sender, EventArgs e)
        {
            InformationButton.Visible = false;
            InformationClicked(this, EventArgs.Empty);
        }
    }
}
