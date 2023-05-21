using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.UnitTests
{
    [TestFixture]
    class CalculatorTests
    {
        [TestCase(1, 2, 3)]
        [TestCase(1, 4, 5)]
        [TestCase(int.MaxValue, 7, int.MaxValue + (long)7)]
        public void Add_ValidInput_ReturnsSuccess(int a, int b, long expectedResult)
        {
            // Act
            var calculator = new Calculator();
            var actualResult = calculator.Add(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        [TestCase(1, 2, -1)]
        [TestCase(1, 4, -3)]
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue - (long)int.MinValue)]
        public void Sub_ValidInput_ReturnsSuccess(int a, int b, long expectedResult)
        {
            // Act
            var calculator = new Calculator();
            var actualResult = calculator.Sub(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        [TestCase(1, 2, 2)]
        [TestCase(1, 4, 4)]
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue * (long)int.MinValue)]
        public void Mul_ValidInput_ReturnsSuccess(int a, int b, long expectedResult)
        {
            // Act
            var calculator = new Calculator();
            var actualResult = calculator.Mul(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        [TestCase(1, 2, (double)1/2)]
        [TestCase(1, 4, (double)1 / 4)]
        public void Div_ValidInput_ReturnsSuccess(int a, int b, double expectedResult)
        {
            // Act
            var calculator = new Calculator();
            var actualResult = calculator.Div(a, b);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        [TestCase(1, 0, "Không được chia cho 0")]
        public void Div_ZeroDivede_ReturnsException(int a, int b, string expectedResult)
        {
            // Act && Assert
            var calculator = new Calculator();

            // Assert
            var ex = Assert.Throws<ArgumentException>(() => calculator.Div(a, b));
            Assert.That(ex.Message, Is.EqualTo(expectedResult));
        }
    }
}
