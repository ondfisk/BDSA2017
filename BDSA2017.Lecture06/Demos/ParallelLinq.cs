using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BDSA2017.Lecture06.Demos
{
    public static class ParallelLinq
    {
        public static void Run()
        {
            var numbers = Enumerable.Range(1, 5000000);
            
            var query = from n in numbers.AsParallel().AsOrdered()
                        where Enumerable.Range(2, (int) Math.Sqrt(n)).All(i => n%i > 0)
                        select n;

            var (primes, duration) = query.Measure();

            Console.WriteLine("Primes: {0}, first: {1}, last: {2}", duration, primes.First(), primes.Last());
        }

        public static (T[] result, TimeSpan duration) Measure<T>(this IEnumerable<T> action)
        {
            var stopwatch = Stopwatch.StartNew();

            var result = action.ToArray();

            stopwatch.Stop();

            return (result, stopwatch.Elapsed);
        }
    }
}
