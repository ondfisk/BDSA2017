using System;
using System.Collections.Generic;

namespace BDSA2017.Lecture02
{
    public class DuckAgeComparer : IComparer<Duck>
    {
        public int Compare(Duck x, Duck y)
        {
            if (x.Age < y.Age)
            {
                return -1;
            }
            else if (x.Age > y.Age)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static Comparison<Duck> Comparison => throw new NotImplementedException();
    }
}
