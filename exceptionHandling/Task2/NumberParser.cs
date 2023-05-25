using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue is null)
            {
                throw new ArgumentNullException(nameof(stringValue),"Value is null");

            }
            var str = stringValue.Trim();

            if (string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str))
            {
                throw new FormatException("Invalid input format");
            }
            
            long number = 0;
            var sign = 1;
            var i = 0;
            
            if (str[0] == '-')
            {
                sign = -1;
                i = 1;
            }
            if (str[0] == '+')
            {
                sign = 1;
                i = 1;
            }
            
            while (i < str.Length)
            {
                var currentNum = str[i] - '0';
                if (currentNum > 9 || currentNum < 0)
                {
                    throw new FormatException("Input must contain only digits");
                }

                number = (number * 10) + currentNum;
                i++;
            }

            number *= sign;
            
            if (number > int.MaxValue || number < int.MinValue)
            {
                throw new OverflowException("Overflow exception");
            }

            Console.WriteLine(number);
            return (int) number;
        }
    }
}