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

            serviceCollection.AddTransient<IAnimal, Cow>();

            serviceCollection.AddTransient<IPublisher, Publisher>();
            serviceCollection.AddTransient<IArchiver, Archiver>();
            serviceCollection.AddTransient<IPeopleRepository, PeopleRepository>();
            serviceCollection.AddTransient<INotifier, Notifier>();
            serviceCollection.AddTransient<Facade>();

            serviceCollection.AddSingleton<IConfig, HardcodedConfig>();

            var connectionString = @"<input_connection_string_here>";
            //serviceCollection.AddScoped<ICharacterRepository>(
            //    s => new AdoNetCharacterRepository(connectionString));
            serviceCollection.AddScoped<ICharacterContext>(s => new CharacterContext(connectionString));
            serviceCollection.AddScoped<ICharacterRepository, EntityFrameworkCharacterRepository>();

            serviceCollection.AddScoped<Bridge>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
