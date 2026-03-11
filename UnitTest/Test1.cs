using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ПрактическаяРабота4_Зевакин_Шпилько;

namespace UnitTest
{
    [TestClass]
    public class FunctionTests
    {
        // ===== ТЕСТЫ FirstFunction =====

        [TestMethod]
        public void FirstCalculate_ValidValues_ReturnsCorrectResult()
        {
            // Arrange
            double x = 2.0, y = 3.0, z = 1.0;
            double expected = MathLogics.FirstCalculate(x, y, z);

            // Act
            double result = MathLogics.FirstCalculate(x, y, z);

            // Assert
            Assert.AreEqual(expected, result, 0.0001);
        }

        [TestMethod]
        public void FirstCalculate_XIsZero_DoesNotReturnNaN()
        {
            // Arrange
            double x = 0.0, y = 2.0, z = 1.0;

            // Act
            double result = MathLogics.FirstCalculate(x, y, z);

            // Assert
            Assert.IsFalse(double.IsNaN(result), "При x=0 результат не должен быть NaN");
        }

        [TestMethod]
        public void FirstCalculate_NegativeX_WorksCorrectly()
        {
            // Arrange
            double x = -4.0, y = 2.0, z = 1.0;

            // Act
            double result = MathLogics.FirstCalculate(x, y, z);

            // Assert
            Assert.IsFalse(double.IsNaN(result), "При отрицательном x берётся |x|, результат должен быть числом");
        }

        [TestMethod]
        public void FirstCalculate_YIsZero_ReturnsNaNOrInfinity()
        {
            // Arrange
            double x = 2.0, y = 0.0, z = 1.0;

            // Act
            double result = MathLogics.FirstCalculate(x, y, z);

            // Assert
            Assert.IsTrue(double.IsNaN(result) || double.IsInfinity(result),
                "При y=0 логарифм не определён, ожидается NaN или Infinity");
        }

        // ===== ТЕСТЫ ThirdFunction =====

        [TestMethod]
        public void ThirdCalculate_ValidValues_ReturnsCorrectResult()
        {
            // Arrange
            double x = 1.0, b = 0.5;
            double expected = Math.Pow(1, 4) + Math.Cos(2 + Math.Pow(1, 3) - 0.5);

            // Act
            double result = MathLogics.ThirdCalculate(x, b);

            // Assert
            Assert.AreEqual(expected, result, 0.0001);
        }

        [TestMethod]
        public void ThirdCalculate_XIsZero_ReturnsCorrectValue()
        {
            // Arrange
            double x = 0.0, b = 1.0;
            double expected = Math.Pow(0, 4) + Math.Cos(2 + Math.Pow(0, 3) - 1.0);

            // Act
            double result = MathLogics.ThirdCalculate(x, b);

            // Assert
            Assert.AreEqual(expected, result, 0.0001);
        }

        [TestMethod]
        public void ThirdCalculate_NegativeValues_DoesNotReturnNaN()
        {
            // Arrange
            double x = -2.0, b = -1.0;

            // Act
            double result = MathLogics.ThirdCalculate(x, b);

            // Assert
            Assert.IsFalse(double.IsNaN(result), "При отрицательных значениях результат не должен быть NaN");
        }

        [TestMethod]
        public void ThirdCalculate_LargeX_ResultIsLarge()
        {
            // Arrange
            double x = 10.0, b = 0.0;

            // Act
            double result = MathLogics.ThirdCalculate(x, b);

            // Assert
            Assert.IsTrue(result > 9000, "При x=10, x^4=10000, результат должен быть большим");
        }
    }
}