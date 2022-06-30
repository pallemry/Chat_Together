using System;
using System.Drawing;
using System.Windows.Forms;

namespace Form_Functions
{
    public partial class InputBox : Form
    {
        private readonly TextBoxInformation[]? _textBoxes;
        public event InputBoxClosed ClosedINBox;
        public TextBox[] TextBoxes { get; }
        public bool PassedAllPredicates { get; private set; }
        public InputBox(string MainTitle, params TextBoxInformation[] textBoxes)
        {
            PassedAllPredicates = textBoxes.Length == 0;
            _textBoxes = textBoxes;
            InitializeComponent();
            Text = MainTitle;
            CenterToScreen();
            if (textBoxes == null || textBoxes.Length == 0)
                throw new ArgumentException("Must be at least one text box");
            TextBoxes = new TextBox[textBoxes.Length];

            #region Initialize TextBoxes
            
            int currentY = 20, x = 0;
            foreach (var box in textBoxes)
            {
                Height += 50;
                ConfirmButton.Location = ConfirmButton.Location with { Y = ConfirmButton.Location.Y + 50 };
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
            var shouldRetry = false;

            for (var index = 0; index < TextBoxes.Length; index++)
            {
                var input = TextBoxes[index];
                var inputText = input.Text;

                if (input.ForeColor == Color.Gray || string.IsNullOrEmpty(inputText.Trim())) continue;
                inputs[index] = inputText;

                var predicates = _textBoxes?[index].PredicatesInformation;

                if (predicates == null || predicates.Length == 0) continue;

                foreach (var predicate in predicates)
                {
                    if (predicate.Predicate(inputText)) 
                        continue;

                    MessageBox.Show(predicate.Message, predicate.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (predicate.ShouldCrash)
                    {
                        Close();
                        return;
                    }
                    // ReSharper disable once RedundantIfElseBlock
                    else
                    {
                        shouldRetry = true;
                        break;
                    }
                    
                }
            }

            if (shouldRetry) return;

            PassedAllPredicates = true;
            ClosedINBox(this, new InputBoxClosedArgs(inputs));
            Close();
        }
    }

    public delegate void InputBoxClosed(object sender, InputBoxClosedArgs args);
}
 