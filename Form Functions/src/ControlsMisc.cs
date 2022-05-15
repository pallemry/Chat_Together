using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace Form_Functions
{
    public delegate bool GetDef();
    public static class ControlsMisc
    {
        public static readonly Dictionary<TextBox, GetDef> IsEmpty = new ();

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
            target.Enter += (sender, args) =>
            {
                if (!placeHolding) return;
                target.Text = string.Empty;
                target.ForeColor = defaultForeColor;
                target.BackColor = defaultBackColor;
                placeHolding = false;
            };
            target.Leave += (sender, args) =>
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
            cb.CheckedChanged += (sender, args) => tb.UseSystemPasswordChar = !cb.Checked;
        }
    }
}
