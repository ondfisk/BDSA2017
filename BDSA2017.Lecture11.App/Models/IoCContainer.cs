using BDSA2017.Lecture11.App.ViewModels;
using BDSA2017.Lecture11.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace BDSA2017.Lecture11.App.Models
{
    public class IoCContainer
    {
        public static IServiceProvider Create() => ConfigureServices();

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddScoped<ICharacterRepository, CharacterRepository>();
            services.AddScoped<ISettings, Settings>();
            services.AddScoped<MainPageViewModel>();
            services.AddScoped<CharacterPageViewModel>();
            services.AddScoped<ISettings, Settings>();
            services.AddScoped<DelegatingHandler, AuthorizedHandler>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            services.AddScoped<INavigationService, NavigationService>();

            return services.BuildServiceProvider();
        }
    }
}
