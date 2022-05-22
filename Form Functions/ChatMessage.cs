using System;
using System.Drawing;
using System.Linq;
using System.Timers;
using System.Windows.Forms;

using Timer = System.Timers.Timer;

namespace Chat_Together
{
    public partial class ChatMessage : UserControl
    {

        public ChatMessage(string message, float width, string? author)
        {
            InitializeComponent();
            Width = (int)(width + 67);
            richTextBox1.Text = message;
            richTextBox1.Width = (int) width - 15;
            richTextBox1.ReadOnly = true;
            richTextBox1.AutoSize = false;
            foreach (var unused in richTextBox1.Lines)
            {
                if (unused is not null && !string.IsNullOrEmpty(unused.Trim())) continue;
                if (richTextBox1.Lines.Length <= 0) continue;
                var lines = richTextBox1.Lines.ToList();
                lines.RemoveAt(lines.Count - 1 );
                richTextBox1.Lines = lines.ToArray();
            }
            var height = (int) CreateGraphics().MeasureString(richTextBox1.Text, richTextBox1.Font, new SizeF(width, 99999)
                                                              , StringFormat.GenericDefault).Height;
            richTextBox1.Height = (int) (height * 1.2);
            
            var fixedMsgHs = richTextBox1.Height + 38;
            var fixedImgHs = UserImage.Location.Y + UserImage.Height + 20;
            Height = fixedMsgHs <= fixedImgHs ? fixedImgHs : fixedMsgHs + 20;
            var w = (int)CreateGraphics().MeasureString(richTextBox1.Text, richTextBox1.Font, new SizeF(width, 99999)
                                                        , StringFormat.GenericDefault).Width + 20;
            var nameW = (int)CreateGraphics().MeasureString(author, Author.Font, new SizeF(9999, 99999)
                                                            , StringFormat.GenericDefault).Width + 20;

            if (w < width)
            {
                richTextBox1.Width = w;
            }
            if (nameW > w)
                Width = nameW + 67;
            else
                Width = w + 67;
        }

        private void LoadDraw(object sender, ElapsedEventArgs e)
        {
            try
            {
                //Invoke((MethodInvoker)delegate
                //{
                //    var bars = richTextBox1.ScrollBars;

                //    // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
                //    while (bars is RichTextBoxScrollBars.Both
                //                   or RichTextBoxScrollBars.Vertical
                //                   or RichTextBoxScrollBars.ForcedVertical
                //                   or RichTextBoxScrollBars.ForcedBoth)
                //    {
                //        richTextBox1.Height += 1;
                //        //if (string.IsNullOrEmpty(richTextBox1.Lines.Last().Trim())) break;
                //        if (richTextBox1.Height > 9999)
                //        {
                //            throw new Exception();
                //        }
                //    }
                //});
                if (IsDisposed) return;
                return;
                var g = CreateGraphics();
                var imgLoc = UserImage.Location;
                g.FillEllipse(new SolidBrush(Color.Red), 
                              new RectangleF(imgLoc.X-10, imgLoc.Y-10, 
                                             UserImage.Width + 20, UserImage.Height + 20));
                g.Dispose();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void ChatMessage_Load(object sender, EventArgs e)
        {
            Timer t = new(100);
            t.Elapsed += LoadDraw;
            //t.Start();
        }
    }
}
