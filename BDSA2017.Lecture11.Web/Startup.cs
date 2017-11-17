using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BDSA2017.Lecture11.Entities;
using Microsoft.EntityFrameworkCore;
using BDSA2017.Lecture11.Common;
using Swashbuckle.AspNetCore.Swagger;
using BDSA2017.Lecture11.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BDSA2017.Lecture11.Web
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
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.DocumentFilter<LowerCaseDocumentFilter>();
            });
            
            services.AddDbContext<FuturamaContext>(o =>
                o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFuturamaContext, FuturamaContext>();
            services.AddScoped<ICharacterRepository, EntityFrameworkCharacterRepository>();

            var options = new AzureAdOptions();
            Configuration.Bind("AzureAd", options);

            //services.AddAuthentication(o =>
            //{
            //    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //})
            //.AddCookie()
            //.AddOpenIdConnect(o =>
            //{
            //    o.Authority = options.Authority;
            //    o.ClientId = options.ClientId;
            //    o.UseTokenLifetime = true;
            //    o.CallbackPath = options.CallbackPath;
            //    o.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        SaveSigninToken = true,
            //        ValidAudience = options.Audience
            //    };
            //});

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.Audience = options.Audience;
                o.Authority = options.Authority;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    SaveSigninToken = true,
                    ValidAudience = options.Audience
                };
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseMvc();
        }
    }
}
