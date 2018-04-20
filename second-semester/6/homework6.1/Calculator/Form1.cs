using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        Calculator calculator = new Calculator();

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void OperationClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (LastCharIsInteger(this.textBox.Text))
            {
                var result = calculator.Eval(this.textBox.Text);
                this.textBox.Text = $"{result} {button.Text} ";
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
                this.textBox.Text += ",";
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
                this.textBox.Text = $"{calculator.Eval(this.textBox.Text)}";
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
