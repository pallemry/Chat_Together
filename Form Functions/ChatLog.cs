using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

using Chat_Together;
// ReSharper disable MemberCanBePrivate.Global


namespace Form_Functions
{
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    public partial class ChatLog : UserControl
    {
        public ChatLog(int width)
        {
            Width = width;
            InitializeComponent();
        }

        public ChatLog()
        {
            InitializeComponent();
        }

        public readonly List<ChatMessage> _meesageRecs = new ();
        public int DefaultX  { get; set; } = 30;
        public int DefaultY  { get; set; } = 30;
        public int DefaultGap { get; set; } = 20;
        public void AddMessage(string text, string? userName,
                               Image userImage, int width)
        {
            var m = new ChatMessage(text, width, userName);
            m.UserImage.Image = OvalImage(userImage);
            m.Author.Text = userName;
            var last = _meesageRecs.Count != 0 ? _meesageRecs.Last() : null;
            m.Location = Equals(last, null)
                ? new Point(DefaultX,
                            DefaultY)
                : new Point(DefaultX, last.Location.Y + last.Height + DefaultGap);
            _meesageRecs.Add(m);
            Controls.Add(m);
        }

        private void ChatLog_Scroll(object sender, ScrollEventArgs e)
        {
            //BackgroundImage = Properties.Resources._3840x2160_lake_dark_night_starry_sky_landscape;
            //BackgroundImageLayout = BackgroundImageLayout == ImageLayout.Stretch ? ImageLayout.Center : ImageLayout.Stretch;
        }
        public static Image OvalImage(Image img)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(0, 0, img.Width + 3, img.Height + 3);
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.SetClip(gp);
                    gr.DrawImage(img, Point.Empty);
                }
            }
            return bmp;
        }
    }
}
