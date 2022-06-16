using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

using Chat_Together;

using Form_Functions.src;

// ReSharper disable MemberCanBePrivate.Global
namespace Form_Functions;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public partial class ChatLog : UserControl
{
    public event Action<object, MessageInformationClickedEventArgs> MessageInformationClicked;

    public ChatLog(int width)
    {
        Width = width;
        InitializeComponent();
    }

    public ChatLog()
    {
        InitializeComponent();
    }

    public readonly List<ChatMessage> _messageRecs = new ();
    public int DefaultX { get; set; } = 30;
    public int DefaultY { get; set; } = 30;
    public int DefaultGap { get; set; } = 20;

    public void AddMessage(string text, string? userName,
                             Image userImage, int width, bool isSystemMessage = false)
    {
        var m = new ChatMessage(text, width, userName, isSystemMessage);
        m.InformationClicked += (_, _) =>
            MessageInformationClicked(m, new MessageInformationClickedEventArgs(m.Author.Text, m.Message));
        m.UserImage.Image = OvalImage(userImage);
        m.Author.Text = userName;
        var last = _messageRecs.Count != 0 ? _messageRecs.Last() : null;
        m.Location = Equals(last, null)
            ? new Point(DefaultX,
                        DefaultY)
            : new Point(DefaultX, last.Location.Y + last.Height + DefaultGap);
        m.ID = isSystemMessage ? -1 : m.ID;
        _messageRecs.Add(m);
        Controls.Add(m);
    }

    public static Image OvalImage(Image img)
    {
        var bmp = new Bitmap(img.Width, img.Height);

        using (var gp = new GraphicsPath())
        {
            gp.AddEllipse(0, 0, img.Width + 3, img.Height + 3);

            using (var gr = Graphics.FromImage(bmp))
            {
                gr.SetClip(gp);
                gr.DrawImage(img, Point.Empty);
            }
        }

        return bmp;
    }
}