using BDSA2017.Lecture10.App.ViewModels;
using BDSA2017.Lecture10.Common;
using BDSA2017.Lecture10.Entities;
using BDSA2017.Lecture10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using Windows.Storage;

namespace BDSA2017.Lecture10.App.Models
{
    public class IoCContainer
    {
        public static IServiceProvider Create() => ConfigureServices();

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var connectionString = $"Data Source={Path.Combine(ApplicationData.Current.LocalFolder.Path, "futurama.db")}";

            services.AddDbContext<FuturamaContext>(options =>
               options.UseSqlite(connectionString));

            services.AddScoped<IFuturamaContext, FuturamaContext>();
            services.AddScoped<ICharacterRepository, EntityFrameworkCharacterRepository>();
            //services.AddScoped<ICharacterRepository, RestCharacterRepository>();
            //services.AddScoped<HttpClient>();
            services.AddScoped<MainPageViewModel>();
            services.AddScoped<CharacterPageViewModel>();

            var provider = services.BuildServiceProvider();

            using (var context = provider.GetService<FuturamaContext>())
            {
                context.Database.EnsureCreated();
            }

            return provider;
        }
    }
}
