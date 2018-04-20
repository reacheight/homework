using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private Calculator calculator;

        private Dictionary<string, double> validIntegerExpressions = new Dictionary<string, double>
        {
            ["3 + 3"] = 6,
            ["3 - 3"] = 0,
            ["-2 + 7"] = 5,
            ["2 * 3"] = 6,
            ["18 / 6"] = 3,
            ["3"] = 3
        };

        private Dictionary<string, double> validDoubleExpressions = new Dictionary<string, double>
        {
            ["3 + 3,6"] = 6.6,
            ["3,34 - 3"] = 0.34,
            ["-2,4 + 7,9"] = 5.5,
            ["0,5 * 3"] = 1.5,
            ["3 / 7"] = 3.0 / 7,
            ["3,8"] = 3.8
        };

        private List<string> InvalidExpressions = new List<string>
        {
            string.Empty,
            "3 3",
            "3 % 3",
            "3 + 3 + 3",
            "not numbers",
            "a + b",
            "a + 3",
            "9 *+ 3",
            "9, + 4",
            ",3 + 5",
            "3.6 + 3"
        };

        [TestInitialize]
        public void Init()
        {
            this.calculator = new Calculator();
        }

        [TestMethod]
        public void CalculatorWorksRightForIntegers()
        {
            foreach (var item in this.validIntegerExpressions)
            {
                Assert.AreEqual(item.Value, this.calculator.Eval(item.Key), 0.000001);
            }
        }

        [TestMethod]
        public void CalculatorWorksRightForDoubles()
        {
            foreach (var item in this.validDoubleExpressions)
            {
                Assert.AreEqual(item.Value, this.calculator.Eval(item.Key), 0.000001);
            }
        }

        [TestMethod]
        public void EvalThrowsExcpectedExceptionForInvalidExpressions()
        {
            foreach (var expression in this.InvalidExpressions)
            {
                void action()
                {
                    this.calculator.Eval(expression);
                }

                Assert.ThrowsException<InvalidExpressionException>((Action) action);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivisionByZeroThrowsExpectedException()
        {
            this.calculator.Eval("3,7 / 0");
        }
    }
}