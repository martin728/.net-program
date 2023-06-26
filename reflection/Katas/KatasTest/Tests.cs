using System;
using NUnit.Framework;

namespace KatasTest
{
    [TestFixture]
    public class Tests
    {
        private Katas.Katas _katas;

        [SetUp]
        public void Setup()
        {
            _katas = new Katas.Katas();
        }
        
        //FizzBuzz
        [TestCase(1, "1")]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        [TestCase(13, "13")]
        [TestCase(30, "FizzBuzz")]
        [TestCase(98, "98")]
        public void FizzBuzz_WithVariousInputs_ReturnsExpectedOutput(int number, string expected)
        {
            // Act
            string result = _katas.FizzBuzz(number);
            
            // Assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void FizzBuzz_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            int num = -1;

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _katas.FizzBuzz(num));
        }
        
        //OddEven
        [TestCase(1, "1")]
        [TestCase(2, "2")]
        [TestCase(3, "3")]
        [TestCase(4, "Even")]
        [TestCase(5, "5")]
        [TestCase(6, "Even")]
        [TestCase(7, "7")]
        [TestCase(8, "Even")]
        [TestCase(9, "Odd")]
        [TestCase(10, "Even")]
        [TestCase(11, "11")]
        [TestCase(12, "Even")]
        [TestCase(13, "13")]
        [TestCase(14, "Even")]
        [TestCase(15, "Odd")]
        [TestCase(16, "Even")]
        [TestCase(17, "17")]
        [TestCase(18, "Even")]
        [TestCase(19, "19")]
        [TestCase(20, "Even")]
        public void CheckNumber_ReturnsExpectedResult(int num, string expectedResult)
        {
            //Act
            string result = _katas.CheckNumber(num);
            
            //Assert
            Assert.AreEqual(expectedResult, result);
        }
        
        [Test]
        public void CheckNumber_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            int num = -1;

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _katas.CheckNumber(num));
        }
        
        //Is Leap Year
        [TestCase(2000, true)]
        [TestCase(2004, true)]
        [TestCase(1900, false)]
        [TestCase(2001, false)]
        [TestCase(2100, false)]
        [TestCase(2024, true)]
        public void IsLeapYear_WhenCalledWithYear_ReturnsExpectedResult(int year, bool expectedResult)
        {
            // Arrange
            bool isLeap;

            // Act
            isLeap = _katas.IsLeapYear(year);

            // Assert
            Assert.AreEqual(expectedResult, isLeap);
        } 
        
        [Test]
        public void IsLeapYear_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            int num = -1;

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _katas.IsLeapYear(num));
        }
    }
}