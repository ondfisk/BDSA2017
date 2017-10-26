using BDSA2017.Lecture08.Models.Animals;
using BDSA2017.Lecture08.Models.Bridge;
using BDSA2017.Lecture08.Models.ChainOfResponsibility;
using BDSA2017.Lecture08.Models.Facade;
using BDSA2017.Lecture08.Models.Game;
using BDSA2017.Lecture08.Models.Singleton;
using BDSA2017.Lecture08.Models.Strategy;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BDSA2017.Lecture08
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = IoCContainer.Provider;

            //var animal = provider.GetService<IAnimal>();

            //animal.Hello();

            //Game.Run();

            //var config = provider.GetService<IConfig>();

            //Console.WriteLine(config.ClientId);

            //var f = provider.GetService<Facade>();

            //var a = new Article
            //{
            //    Title = "DeepThroat",
            //    Author = "Woodward and Bernstein"
            //};

            //f.Publish(a);

            //ChainOfResponsibility.Run();

            //Strategy.Run();
            var connectionString = "";

            var bridge = provider.GetService<Bridge>();

            bridge.PrintAll().Wait();
        }
    }
}
