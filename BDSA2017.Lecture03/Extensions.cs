using System;
using System.Collections.Generic;
using System.Text;

namespace BDSA2017.Lecture03
{
    public static class Extensions
    {
        public static void Print<T>(this IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}
