using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private const string commaString = ",";
        private Calculator calculator = new Calculator();

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void OperationClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (LastCharIsInteger(this.textBox.Text))
            {
                (var result, bool success) = this.Eval();
                this.textBox.Text = success ? $"{result} {button.Text} " : string.Empty;
            }
        }

        private void DigitButtonClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            this.textBox.Text += button.Text;
        }

        private void CommaButtonClick(object sender, EventArgs e)
        {
            if (LastCharIsInteger(this.textBox.Text))
            {
                this.textBox.Text += commaString;
            }
        }

        private void DeleteButtonClick(object sender, EventArgs e)
        {
            var length = this.textBox.Text.Length;

            if (length <= 0)
            {
                return;
            }
            
            var lastChar = this.textBox.Text[length - 1];
            if (LastCharIsInteger(this.textBox.Text) || lastChar == ',')
            {
                this.textBox.Text = this.textBox.Text.Remove(length - 1);
            }
            else
            {
                this.textBox.Text = this.textBox.Text.Remove(length - 3);
            }
        }

        private void ClearButtonClick(object sender, EventArgs e)
        {
            this.textBox.Text = string.Empty;
        }

        private void EvalButtonClick(object sender, EventArgs e)
        {
            if (LastCharIsInteger(this.textBox.Text))
            {
                this.textBox.Text = this.Eval().value;
            }
        }

        private (string value, bool success) Eval()
        {
            try
            {
                return (calculator.Eval(this.textBox.Text).ToString(), true);
            }
            catch (Exception ex)
            {
                if (ex is InvalidExpressionException || ex is DivideByZeroException)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                    return (string.Empty, false);
                }

                throw;
            }
        }

        private static bool LastCharIsInteger(string expression)
        {
            var length = expression.Length;
            if (length == 0)
            {
                return false;
            }

            var lastChar = expression.Substring(length - 1);
            return int.TryParse(lastChar, out int value);
        }
    }
}
