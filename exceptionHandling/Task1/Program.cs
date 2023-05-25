using System;
using Task2;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Console.WriteLine("Enter");
            // var input = Console.ReadLine();
            // if (string.IsNullOrEmpty(input))
            // {
            //     throw new ArgumentOutOfRangeException(nameof(input), "Input length must be positive.");
            // }
            // Console.WriteLine(input.Substring(0, 1));

            var num = new NumberParser();
            num.Parse("-9999999999999999");
        }
    }
}