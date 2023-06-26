using System;
using NUnit.Framework;

namespace KatasTest
{
    [TestFixture]
    public class Tests
    {
        private Katas.Katas _fizzbuzz;

        [SetUp]
        public void Setup()
        {
            _fizzbuzz = new Katas.Katas();
        }

        [TestCase(1, "1")]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        [TestCase(13, "13")]
        [TestCase(30, "FizzBuzz")]
        [TestCase(98, "98")]
        public void FizzBuzz_WithVariousInputs_ReturnsExpectedOutput(int number, string expected)
        {
            string result = _fizzbuzz.FizzBuzz(number);
            Assert.AreEqual(expected, result);
        }
    }
}