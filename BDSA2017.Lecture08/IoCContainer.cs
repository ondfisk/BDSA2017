using System;
using Microsoft.Extensions.DependencyInjection;
using BDSA2017.Lecture08.Models.Animals;
using BDSA2017.Lecture08.Models.Bridge;
using BDSA2017.Lecture08.Models.Singleton;
using BDSA2017.Lecture08.Models.Facade;

namespace BDSA2017.Lecture08
{
    public class IoCContainer
    {
        public static IServiceProvider Provider { get; }

        static IoCContainer()
        {
            Provider = ConfigureServices();
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
