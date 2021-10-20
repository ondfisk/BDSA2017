using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BDSA2017.Lecture10.Entities;
using Microsoft.EntityFrameworkCore;
using BDSA2017.Lecture10.Models;
using BDSA2017.Lecture10.Common;
using BDSA2017.Lecture10.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Hosting;

namespace BDSA2017.Lecture10.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(o =>
            {
                o.LowercaseUrls = true;
            });

            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                //.RequireRole("Admin", "SuperUser")
                .Build();

            services.Configure<MvcOptions>(o =>
            {
                o.Filters.Add(new RequireHttpsAttribute());
                o.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.DocumentFilter<LowerCaseDocumentFilter>();
            });
            
            services.AddDbContext<FuturamaContext>(o =>
                o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFuturamaContext, FuturamaContext>();
            services.AddScoped<ICharacterRepository, EntityFrameworkCharacterRepository>();

            var options = new AzureAdOptions();
            Configuration.Bind("AzureAd", options);

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(o =>
            {
                o.ClientId = options.ClientId;
                o.Authority = $"{options.Instance}{options.TenantId}";
                o.UseTokenLifetime = true;
                o.CallbackPath = options.CallbackPath;
            })
            .AddJwtBearer(o =>
            {
                o.Audience = options.ClientId;
                o.Authority = $"{options.Instance}{options.TenantId}";
            })
            .AddCookie();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = new RewriteOptions().AddRedirectToHttps();
            app.UseRewriter(options);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
            });

            app.UseAuthentication();

            app.UseStaticFiles();
        }
    }
}
