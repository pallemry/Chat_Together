using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Form_Functions
{
    public delegate bool GetDef();
    public static class ControlsMisc
    {
        public static readonly Dictionary<TextBox, GetDef> IsEmpty = new ();
        public static readonly string LogInInformationConfigPath = $"{GetAppDataPath()}\\LogInInformation.config";

        public static void InitializePanelResize(this Panel panel, Form owner, Control control, params Control[] controls)
        {
            var a = controls.ToList();
            a.Add(control);
            foreach (var c in a)
            {
                c.Resize += (_, _) => {
                    var previousPanelHeight = panel.Height;
                    panel.Height = c.Location.Y + c.Height;
                    owner.Height += panel.Height - previousPanelHeight;
                };
            }
        }

        public static XmlDocument LoadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(LogInInformationConfigPath);
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void ApplyMaxLength(int maxLength, params TextBoxBase[] controls)
        {
            foreach (var control in controls)
            {
                control.MaxLength = maxLength;
            }
        }

        /// <summary>Extension for textboxes that enables placeholder text in .NET Framework (4.7.2), unlike .NET Core which has this feature auto-implemented as a property
        /// under the 'Misc' category </summary> 
        public static void InitializePlaceHolder(this TextBox target, string phText, Color defaultForeColor,
            Color defaultBackColor, Color defaultPhForeColor, Color defaultPhBackColor)
        {
            // ReSharper disable once EnforceIfStatementBraces
            IsEmpty.Add(target, () => string.IsNullOrEmpty(target.Text?.Trim()) ||
                                      target.ForeColor == defaultPhForeColor);
            target.Text = phText;
            target.ForeColor = defaultPhForeColor;
            target.BackColor = defaultPhBackColor;
            var placeHolding = true;
            target.Enter += (_, _) =>
            {
                if (!placeHolding) return;
                target.Text = string.Empty;
                target.ForeColor = defaultForeColor;
                target.BackColor = defaultBackColor;
                placeHolding = false;
            };
            target.Leave += (_, _) =>
            {
                if (!string.IsNullOrEmpty(target.Text.Trim())) return;
                target.ForeColor = defaultPhForeColor;
                target.BackColor = defaultPhBackColor;
                target.Text = phText;
                placeHolding = true;
            };
        }
        public static void InitializeLinkLabel(this Label target, FontStyle linkVisualStyle, float linkTextSize) =>
            InitializeLinkLabel(target, linkVisualStyle, linkTextSize, target.ForeColor, target.BackColor);
        public static void InitializeLinkLabel(this Label target, float linkTextSize,
                                               Color linkColor) =>
            InitializeLinkLabel(target, FontStyle.Regular, linkTextSize, linkColor, target.BackColor);
        public static void InitializeLinkLabel(this Label target, FontStyle linkVisualStyle, float linkTextSize,
                                               Color linkColor) =>
            InitializeLinkLabel(target, linkVisualStyle, linkTextSize, linkColor, target.BackColor);
        public static void InitializeLinkLabel(this Label target, FontStyle linkVisualStyle, float linkTextSize, Color linkForeColor, Color linkBackColor)
        {
            var originFont = target.Font;
            var originBackC = target.BackColor;
            var originForeC = target.ForeColor;
            target.MouseEnter += (_, _) => {
                var f = target.Font;
                f = new(f.Name, linkTextSize, linkVisualStyle);
                target.Font = f;
                target.ForeColor = linkForeColor;
                target.BackColor = linkBackColor;
            };
            target.MouseLeave += (_, _) => {
                target.Font = originFont;
                target.ForeColor = originForeC;
            };

        }

        public static void LinkPasswordWithCheckBox(TextBox tb, CheckBox cb)
        {
            cb.CheckedChanged += (_, _) => tb.UseSystemPasswordChar = !cb.Checked;
        }

        private static string osUserName = Environment.UserName;
        public static string GetAppDataPath() => $"C:\\Users\\{Environment.UserName}\\AppData\\Local\\Chat Together";
        public static string GetImageResourcesPath() => $"{GetAppDataPath()}\\resources\\UserImageProfiles";

    }
}
