using System;
using Microsoft.Extensions.DependencyInjection;
using BDSA2017.Lecture08.Lib.Animals;
using BDSA2017.Lecture08.Lib.Bridge;
using BDSA2017.Lecture08.Lib.Singleton;
using BDSA2017.Lecture08.Lib.Facade;

namespace BDSA2017.Lecture08.App
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

            serviceCollection.AddTransient<IAnimalService, AnimalService>();
            serviceCollection.AddTransient<IAnimal, Cow>();

            var instance = HardcodedConfig.Instance;
            serviceCollection.AddSingleton<IConfig>(instance);

            //serviceCollection.AddSingleton<IConfig, HardcodedConfig>();

            serviceCollection.AddTransient<IPublisher, Publisher>();
            serviceCollection.AddTransient<IArchiver, Archiver>();
            serviceCollection.AddTransient<IPeopleRepository, PeopleRepository>();
            serviceCollection.AddTransient<INotifier, Notifier>();
            serviceCollection.AddTransient<Facade>();

            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Futurama;Integrated Security=SSPI;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            serviceCollection.AddScoped<ICharacterContext, CharacterContext>(e => new CharacterContext(connectionString));
            serviceCollection.AddScoped<ICharacterRepository, EntityFrameworkCharacterRepository>();
            serviceCollection.AddScoped<Bridge>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
