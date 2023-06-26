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
            number.ToString();

        //The OddEven Kata
        public string CheckNumber(int num)
        {
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

            if (isPrime)
            {
                return num.ToString();
            }
            else if (num % 2 == 0)
            {
                return "Even";
            }
            else
            {
                return "Odd";
            }        
        }
    }
}