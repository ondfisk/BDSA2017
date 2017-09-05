using System;
using System.Collections.Generic;

namespace BDSA2017.Lecture02
{
    public class CollectionUtilities
    {
        public static IEnumerable<int> GetEven(IEnumerable<int> list)
        {
            foreach (var number in list)
            {
                if (number % 2 == 0)
                {
                    yield return number;
                }
            }
        }

        public static bool Find(int number, params int[] list)
        {
            foreach (var n in list)
            {
                if (n == number)
                {
                    return true;
                }
            }

            return false;
        }

        public static ISet<int> Unique(IEnumerable<int> numbers)
        {
            return new HashSet<int>(numbers);
        }

        public static IEnumerable<int> Reverse(IEnumerable<int> numbers)
        {
            return new Stack<int>(numbers);
        }

        public static void Sort(List<Duck> ducks, IComparer<Duck> comparer = null)
        {
            ducks.Sort(comparer);
        }

        public static IDictionary<int, Duck> ToDictionary(IEnumerable<Duck> ducks)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Duck> GetOlderThan(IEnumerable<Duck> ducks, int age)
        {
            throw new NotImplementedException();
        }
    }
}
