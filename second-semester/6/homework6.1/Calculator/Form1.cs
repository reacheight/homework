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
        public CalculatorForm()
        {
            InitializeComponent();
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

        private void OnOperatorClick(char operatorChar)
        {
            if (LastCharIsInteger(this.textBox.Text))
            {
                var result = calculator.Eval(this.textBox.Text);
                this.textBox.Text = $"{result} {operatorChar} ";
            }
        }

        Calculator calculator = new Calculator();

        private void DigitButtonClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            this.textBox.Text += button.Text;
        }

        private void buttonComma_Click(object sender, EventArgs e)
        {
            if (LastCharIsInteger(this.textBox.Text))
            {
                this.textBox.Text += ",";
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var length = this.textBox.Text.Length;
            if (length > 0)
            {
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
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.textBox.Text = string.Empty;
        }

        private void buttonAddition_Click(object sender, EventArgs e)
        {
            this.OnOperatorClick('+');
        }

        private void buttonSubtraction_Click(object sender, EventArgs e)
        {
            this.OnOperatorClick('-');
        }

        private void buttonMultiplication_Click(object sender, EventArgs e)
        {
            this.OnOperatorClick('*');
        }

        private void buttonDivision_Click(object sender, EventArgs e)
        {
            this.OnOperatorClick('/');
        }

        private void buttonEval_Click(object sender, EventArgs e)
        {
            if (LastCharIsInteger(this.textBox.Text))
            {
                this.textBox.Text = $"{calculator.Eval(this.textBox.Text)}";
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Decimal)
            {
                this.textBox.Text += e.KeyValue;
            }
        }
    }
}
