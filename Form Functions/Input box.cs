using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_Functions
{
   

    public partial class Input_box : Form
    {
        
        public event InputBoxClosed ClosedINBox;
        public TextBox[] TextBoxes { get; private set; }
        public Input_box(string MainTitle, params TextBoxInformation[]? textBoxes)
        {
            InitializeComponent();
            Text = MainTitle;
            if (textBoxes == null || textBoxes.Length == 0)
                throw new ArgumentException("Must be at least one text box");
            TextBoxes = new TextBox[textBoxes.Length];

            #region Initialize TextBoxes
            
            int currentY = 20, x = 0;
            foreach (var box in textBoxes)
            {
                Height += 50;
                ConfirmButton.Location = new Point(ConfirmButton.Location.X, ConfirmButton.Location.Y + 50);
                var tb = new TextBox();
                var lb = new Label();
                Controls.Add(tb);
                Controls.Add(lb);
                tb.Visible = true;
                lb.Visible = true;
                lb.Text = box.Title;
                lb.Location = new Point(20, currentY);
                currentY += 20;
                tb.Location = new Point(20, currentY);
                currentY += 30;
                tb.InitializePlaceHolder(box.PlaceHolderText, Color.Black, Color.White, Color.Gray, Color.White);
                TextBoxes[x] = tb;
                x++;
            }

            #endregion

        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            var inputs = new string[TextBoxes.Length];

            for (var index = 0; index < TextBoxes.Length; index++)
            {
                var input = TextBoxes[index];
                if (input.ForeColor == Color.Gray || string.IsNullOrEmpty(input.Text.Trim())) continue;
                inputs[index] = input.Text;
            }
            ClosedINBox(this, new InputBoxClosedArgs(inputs));
            Close();
        }
    }
    public struct TextBoxInformation
    {
        public string Title { get; set; }
        public string PlaceHolderText { get; set; }

        public TextBoxInformation(string title, string placeHolderText)
        {
            Title = title;
            PlaceHolderText = placeHolderText;

        }
    }

    public delegate void InputBoxClosed(object sender, InputBoxClosedArgs args);

    public class InputBoxClosedArgs : EventArgs
    {
        public string[] Inputs { get; }

        public InputBoxClosedArgs(string[] inputs) => Inputs = inputs;
    }
}
 