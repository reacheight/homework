namespace Calculator
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Class that implements calculator form
    /// </summary>
    public partial class CalculatorForm : Form
    {
        /// <summary>
        /// Comma string
        /// </summary>
        private const string CommaString = ",";

        /// <summary>
        /// Object that performs calculating
        /// </summary>
        private Calculator calculator = new Calculator();

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatorForm"/> class.
        /// </summary>
        public CalculatorForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Indicates whether last character of the string is integer
        /// </summary>
        /// <param name="expression">given string</param>
        /// <returns>True if last character of the string is integer, false otherwise</returns>
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

        /// <summary>
        /// Operation button click handler
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void OperationButtonClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (LastCharIsInteger(this.textBox.Text))
            {
                (var result, bool success) = this.Eval();
                this.textBox.Text = success ? $"{result} {button.Text} " : string.Empty;
            }
        }

        /// <summary>
        /// Digit button click handler
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void DigitButtonClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            this.textBox.Text += button.Text;
        }

        /// <summary>
        /// Comma button click handler
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void CommaButtonClick(object sender, EventArgs e)
        {
            if (LastCharIsInteger(this.textBox.Text))
            {
                this.textBox.Text += CommaString;
            }
        }

        /// <summary>
        /// Delete button click handler
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
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

        /// <summary>
        /// Clear button click hangler
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
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

        /// <summary>
        /// Evaluates current calculator expression
        /// </summary>
        /// <returns>value of evaluating as string, boolean status code</returns>
        private (string value, bool success) Eval()
        {
            try
            {
                return (this.calculator.Eval(this.textBox.Text).ToString(), true);
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
    }
}
