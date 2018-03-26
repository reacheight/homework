﻿namespace ExpressionTree.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    [TestClass]
    public class ParseTreeTests
    {
        private const double Delta = 0.000001;
        private Dictionary<string, (double value, string infixNotation)> table = new Dictionary<string, (double value, string infixNotation)>
        {
            ["(+ 2 3)"] = (5, "(2 + 3)"),
            ["(- 10 4)"] = (6, "(10 - 4)"),
            ["(* 3 4)"] = (12, "(3 * 4)"),
            ["(/ 10 4)"] = (2.5, "(10 / 4)"),
            ["(+ (* 3 4) (- 10 5))"] = (17, "((3 * 4) + (10 - 5))"),
            ["(- (/ 20 4) (+ 10.5 5))"] = (-10.5, "((20 / 4) - (10,5 + 5))"),
            ["(* (+ (- 15 (* 2 5)) 6) (+ (- (* 4 1) 3) (+ (/ 7 (+ 3 4)) 1)))"] =
            (33, "(((15 - (2 * 5)) + 6) * (((4 * 1) - 3) + ((7 / (3 + 4)) + 1)))"),
            ["(* 5 (+ 8 4))"] = (60, "(5 * (8 + 4))"),
            ["(/ 5 (/ 1 2))"] = (10, "(5 / (1 / 2))"),
            ["(/ 0 125)"] = (0, "(0 / 125)"),
            ["(* 3 (/ 1 2))"] = (1.5, "(3 * (1 / 2))"),
            ["(* (* 2 (* 2 (* 2 (* 1 2)))) (+ 1 2))"] = (48, "((2 * (2 * (2 * (1 * 2)))) * (1 + 2))")
        };

        [TestMethod]
        public void TestTreeValue()
        {
            foreach(var key in this.table.Keys)
            {
                var tree = new ParseTree(key);
                Assert.AreEqual(table[key].value, tree.Value, Delta);
            }
        }

        [TestMethod]
        public void TestTreeInfixNotation()
        {
            foreach (var key in this.table.Keys)
            {
                var tree = new ParseTree(key);
                Assert.AreEqual(table[key].infixNotation, tree.InfixNotation);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivisionByZeroWillThrowExpectedException()
        {
            var tree = new ParseTree("(/ 123 0)");
            var result = tree.Value;
        }
    }
}