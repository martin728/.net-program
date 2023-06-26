using System;

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
    }
}