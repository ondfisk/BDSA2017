using BDSA2017.Lecture10.App.ViewModels;
using BDSA2017.Lecture10.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace BDSA2017.Lecture10.App.Models
{
    public class IoCContainer
    {
        public static IServiceProvider Create() => ConfigureServices();

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddScoped<ICharacterRepository, RestCharacterRepository>();
            services.AddScoped<HttpClient>();
            services.AddScoped<MainPageViewModel>();
            services.AddScoped<CharacterPageViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
