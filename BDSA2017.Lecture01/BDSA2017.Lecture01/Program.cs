using System;
using static System.Console;

namespace BDSA2017.Lecture01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var name = args != null && args.Length > 0 ?
                args[0] :
                "World";

            WriteLine($"Hello, {name}!");
            WriteLine("Give me a number: ");
            
            var input = ReadLine();
            if (!int.TryParse(input, out var number))
            {
                WriteLine($"That is not a number!");
            }
            else if (number == 42)
            {
                WriteLine($"You gave me the right number: {number}!");
            }
            else
            {
                WriteLine($"You gave me the wrong number: {number}!");
            }
        }
    }
}
