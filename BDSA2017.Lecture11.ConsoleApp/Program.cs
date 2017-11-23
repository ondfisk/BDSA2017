using System;
using System.Linq;

namespace BDSA2017.Lecture11.ConsoleApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            System.Linq.Enumerable.Select(args, e => e.Length == 2);
            args.Select(e => e.Length == 2);

        }

        static string ToWftString(this object obj)
        {
            return "WTF";
        }
    }
}
