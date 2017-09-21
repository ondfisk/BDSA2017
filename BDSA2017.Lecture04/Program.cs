using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BDSA2017.Lecture04
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new FuturamaContext())
            {
                var actor = context.Actors.FirstOrDefault();

                Console.WriteLine(actor.Name);
            }
        }
    }
}
