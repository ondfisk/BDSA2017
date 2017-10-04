using System;
using System.Threading;

namespace BDSA2017.Lecture06.Demos
{
    public class Threads
    {
        /// <summary>
        /// Run an open Process Explorer...
        /// </summary>
        public static void SpawnThread()
        {
            Console.WriteLine("Hello Threads");
        }

        /// <summary>
        /// Run an open Process Explorer...
        /// </summary>
        /// <param name="number"></param>
        public static void SpawnMultipleThreads(int numberOfThreads)
        {
            Console.WriteLine("Hello Threads");
            Console.WriteLine("Press any key to continue . . .");
            Console.ReadKey();

            var t = new Thread[numberOfThreads];
            for (var i = 0; i < t.Length; i++)
            {
                var n = i + 1;
                t[i] = new Thread(() =>
                {
                    Console.WriteLine("I'm thread no. {0}", n);
                    Thread.Sleep(TimeSpan.FromSeconds(20));
                });
                t[i].Start();
            }
        }

        public static void Overlapping()
        {
            var t = new Thread(WriteX);
            t.Start();

            for (var i = 0; i < 40; i++)
            {
                Console.Write('Y');
            }
        }

        private static void WriteX()
        {
            for (var i = 0; i < 40; i++)
            {
                Console.Write('X');
            }
        }

        private static void Write(char c, int count)
        {
            for (var i = 0; i < count; i++)
            {
                Console.Write(c);
            }
            Console.WriteLine();
        }

        private static void Write(object c)
        {
            Write((char)c, 40);
        }

        public static void OverlappingWithArguments()
        {
            var t1 = new Thread(WriteX);
            t1.Start();

            var t2 = new Thread(Write);
            t2.Start('Z');

            var t3 = new Thread(() => Write('Y', 40));
            t3.Start();
        }

        public static void Join()
        {
            var t1 = new Thread(
                () =>
                {
                    for (var i = 0; i < 100; i++)
                    {
                        Console.WriteLine("Hello");
                    }
                });

            t1.Start();
            t1.Join();
            Console.WriteLine("Thread t has ended");
            Thread.Sleep(3000);
            Console.WriteLine("Done waiting...");
        }
    }
}
