using System;
using System.Linq;

namespace Katas
{
    public class Katas
    {
        //The FizzBuzz Kata
        public string FizzBuzz(int number) =>
            number % 15 == 0 ? "FizzBuzz" :
            number % 3 == 0 ? "Fizz" :
            number % 5 == 0 ? "Buzz" :
            number > 100 || number <= 0 ? throw new ArgumentOutOfRangeException() :
            number.ToString();

        //The OddEven Kata
        public string CheckNumber(int num)
        {
            if (num <= 0 || num > 100)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            if (num < 2)
            {
                return num.ToString();
            }

            bool isPrime = true;
            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0)
                {
                    isPrime = false;
                    break;
                }
            }

            return isPrime ? num.ToString() : (num % 2 == 0 ? "Even" : "Odd");
        }
        
        //The Leap Year Kata
        public bool IsLeapYear(int year)
        {
            if (year <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            return (year % 4 == 0) && (year % 100 != 0) || (year % 400 == 0);
        }
    }
}