using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA2017.Lecture08.Lib.Animals
{
    public class Cow : IAnimal
    {
        public void Hello()
        {
            Console.WriteLine("Mooh");
        }
    }
}
