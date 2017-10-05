using System;
using System.Threading.Tasks;

namespace BDSA2017.Lecture06
{
    public class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();

            //Demos.Threads.SpawnMultipleThreads(1000);
            //Demos.Threads.Overlapping();
            //Demos.Threads.OverlappingWithArguments();
            //Demos.Threads.Join();

            //Demos.RaceCondition.Race();
            //Demos.FixedRace.Race();
            //Demos.BehindTheScenes.Race();

            //Demos.Deadlock.Run();
            //Demos.Deadlock.RunWithComments();

            //Demos.Tasks.TaskFactory();
            //Demos.Tasks.Wait();
            //Demos.Tasks.WaitAll();
            //Demos.Tasks.Attached();
            //Demos.Tasks.Continuation();
            //Demos.Tasks.Result();
            //Demos.Tasks.Cancellation();
            //Demos.Tasks.ResultCancelled();
            //Demos.Tasks.Fail();

            //Demos.TaskParallelLibrary.For();
            //Demos.TaskParallelLibrary.ForEach();
            //Demos.TaskParallelLibrary.Invoke();

            //Demos.ParallelLinq.Run();

            //Demos.ConcurrentCollections.Race();

            //Console.WriteLine("Press any key to continue . . .");
            //Console.ReadKey();
        }

        private static async Task MainAsync(string[] args)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            Console.WriteLine("Waited for a couple of seconds");
        }
    }
}
