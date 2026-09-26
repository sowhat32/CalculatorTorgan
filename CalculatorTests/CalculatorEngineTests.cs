using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLibrary;
using System;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorEngineTests
    {
        private CalculatorEngine _calculator;

        [TestInitialize]
        public void Setup()
        {
            _calculator = new CalculatorEngine();
        }

        [TestMethod]
        public void Test_Addition()
        {
            double result = _calculator.Calculate(5, 3, "+");
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void Test_Subtraction()
        {
            double result = _calculator.Calculate(10, 4, "-");
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Test_Multiplication()
        {
            double result = _calculator.Calculate(7, 6, "*");
            Assert.AreEqual(42, result);
        }

        [TestMethod]
        public void Test_Division()
        {
            double result = _calculator.Calculate(15, 3, "/");
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Test_DivisionByZero()
        {
            _calculator.Calculate(10, 0, "/");
        }

        [TestMethod]
        public void Test_Power()
        {
            double result = _calculator.Calculate(2, 3, "^");
            Assert.AreEqual(8, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_UnknownOperation()
        {
            _calculator.Calculate(5, 3, "%");
        }


        [TestMethod]
        public void Test_Evaluate_WithUnaryMinusAndBrackets()
        {
            Assert.AreEqual(9, _calculator.Evaluate("-2^2+(1+4)"));
        }

        [TestMethod]
        public void Test_Evaluate_WithPriority()
        {
            Assert.AreEqual(14, _calculator.Evaluate("2+3*4"));
        }

        [TestMethod]
        public void Test_Evaluate_WithBrackets()
        {
            Assert.AreEqual(20, _calculator.Evaluate("(2+3)*4"));
        }
    }
}