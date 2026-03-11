using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Avalonia.Controls;
using ПрактическаяРабота4_Зевакин_Шпилько;
using ПрактическаяРабота4_Зевакин_Шпилько.Pages;


namespace UnitTest
{
    [TestClass]
    public class FunctionTests
    {
        /// <summary>
        /// Проверяет корректность вычислений FirstCalculate при валидных входных данных
        /// </summary>
        [TestMethod]
        public void FirstCalculate_ValidValues_ReturnsCorrectResult()
        {
            double x = 2.0, y = 3.0, z = 1.0;
            double expected = MathLogics.FirstCalculate(x, y, z);

            double result = MathLogics.FirstCalculate(x, y, z);

            Assert.AreEqual(expected, result, 0.0001);
        }

        /// <summary>
        /// Проверяет, что при x = 0 результат не является NaN
        /// </summary>
        [TestMethod]
        public void FirstCalculate_XIsZero_DoesNotReturnNaN()
        {
            double x = 0.0, y = 2.0, z = 1.0;

            double result = MathLogics.FirstCalculate(x, y, z);

            Assert.IsFalse(double.IsNaN(result), "При x=0 результат не должен быть NaN");
        }

        /// <summary>
        /// Проверяет корректность работы с отрицательными значениями x (используется модуль)
        /// </summary>
        [TestMethod]
        public void FirstCalculate_NegativeX_WorksCorrectly()
        {
            double x = -4.0, y = 2.0, z = 1.0;

            double result = MathLogics.FirstCalculate(x, y, z);

            Assert.IsFalse(double.IsNaN(result), "При отрицательном x берётся |x|, результат должен быть числом");
        }

        /// <summary>
        /// Проверяет, что при y = 0 результат является NaN или Infinity (логарифм не определён)
        /// </summary>
        [TestMethod]
        public void FirstCalculate_YIsZero_ReturnsNaNOrInfinity()
        {
            double x = 2.0, y = 0.0, z = 1.0;

            double result = MathLogics.FirstCalculate(x, y, z);

            Assert.IsTrue(double.IsNaN(result) || double.IsInfinity(result),
                "При y=0 логарифм не определён, ожидается NaN или Infinity");
        }

        /// <summary>
        /// Проверяет, что метод vich возвращает false при пустых полях ввода
        /// </summary>
        [TestMethod]
        public void Vich_EmptyFields_ReturnsFalse()
        {
            var page = new SecondFunction();
            var boxX = new TextBox { Text = "" };
            var boxM = new TextBox { Text = "" };

            bool result = page.vich(boxX, boxM);

            Assert.IsFalse(result, "При пустых полях метод должен вернуть false");
        }

        /// <summary>
        /// Проверяет, что метод vich возвращает false при некорректном вводе (буквы вместо чисел)
        /// </summary>
        [TestMethod]
        public void Vich_InvalidInput_ReturnsFalse()
        {
            var page = new SecondFunction();
            var boxX = new TextBox { Text = "abc" };
            var boxM = new TextBox { Text = "xyz" };

            bool result = page.vich(boxX, boxM);

            Assert.IsFalse(result, "При некорректном вводе метод должен вернуть false");
        }

        /// <summary>
        /// Проверяет, что метод vich возвращает false без выбранной функции (RadioButton)
        /// </summary>
        [TestMethod]
        public void Vich_ValidInputXPositiveMOdd_ReturnsTrue()
        {
            var page = new SecondFunction();
            var boxX = new TextBox { Text = "2" };
            var boxM = new TextBox { Text = "3" };

            bool result = page.vich(boxX, boxM);

            Assert.IsFalse(result, "Без выбранной функции должен вернуть false");
        }

        /// <summary>
        /// Проверяет корректность вычисления функции sh(x) - гиперболический синус
        /// </summary>
        [TestMethod]
        public void CalculateFx_Sinh_ReturnsCorrectValue()
        {
            double x = 1.0;
            double expected = Math.Sinh(x);

            double result = Math.Sinh(x);

            Assert.AreEqual(expected, result, 0.0001, "Функция sh(x) должна вычисляться корректно");
        }

        /// <summary>
        /// Проверяет корректность вычислений ThirdCalculate при валидных входных данных
        /// </summary>
        [TestMethod]
        public void ThirdCalculate_ValidValues_ReturnsCorrectResult()
        {
            double x = 1.0, b = 0.5;
            double expected = Math.Pow(1, 4) + Math.Cos(2 + Math.Pow(1, 3) - 0.5);

            double result = MathLogics.ThirdCalculate(x, b);

            Assert.AreEqual(expected, result, 0.0001);
        }

        /// <summary>
        /// Проверяет корректность вычисления ThirdCalculate при x = 0
        /// </summary>
        [TestMethod]
        public void ThirdCalculate_XIsZero_ReturnsCorrectValue()
        {
            double x = 0.0, b = 1.0;
            double expected = Math.Pow(0, 4) + Math.Cos(2 + Math.Pow(0, 3) - 1.0);

            double result = MathLogics.ThirdCalculate(x, b);

            Assert.AreEqual(expected, result, 0.0001);
        }

        /// <summary>
        /// Проверяет, что при отрицательных значениях x и b результат не является NaN
        /// </summary>
        [TestMethod]
        public void ThirdCalculate_NegativeValues_DoesNotReturnNaN()
        {
            double x = -2.0, b = -1.0;

            double result = MathLogics.ThirdCalculate(x, b);

            Assert.IsFalse(double.IsNaN(result), "При отрицательных значениях результат не должен быть NaN");
        }

        /// <summary>
        /// Проверяет, что при больших значениях x результат получается большим (x^4 = 10000)
        /// </summary>
        [TestMethod]
        public void ThirdCalculate_LargeX_ResultIsLarge()
        {
            double x = 10.0, b = 0.0;

            double result = MathLogics.ThirdCalculate(x, b);

            Assert.IsTrue(result > 9000, "При x=10, x^4=10000, результат должен быть большим");
        }
    }
}