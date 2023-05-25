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
            
            var num = 0;
            var sign = 1;
            int i = 0;
            int res = 0;
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
                int currentNum = str[i] - '0';
                if (currentNum > 9 || currentNum < 0)
                {
                    throw new FormatException("Input must contain only digits");
                }

                num = (num * 10) + currentNum;
                i++;
            }

            res = num * sign;
            
            if (res > Int32.MaxValue || res < int.MinValue)
            {
                throw new OverflowException("Overflow exception");
            }

            Console.WriteLine(res);
            return res;
        }
    }
}