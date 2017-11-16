# Lecture 10

## Enable Swagger

```ps
PS> Install-Package Swashbuckle.AspNetCore
```

`Startup.cs`

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            c.DocumentFilter<LowerCaseDocumentFilter>();
        });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
        });
    }
}
```

`LowerCaseDocumentFilter.cs`

```csharp
public class LowerCaseDocumentFilter : IDocumentFilter
{
    public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Paths = swaggerDoc.Paths.ToDictionary(d => d.Key.ToLower(), d => d.Value);
    }
}
```

`launchSettings.json`

```json
"launchUrl": "http://localhost:[port]/swagger",
```

## Require SSL

`Startup.cs`

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<MvcOptions>(o =>
        {
            o.Filters.Add(new RequireHttpsAttribute());
        });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        var options = new RewriteOptions().AddRedirectToHttps();
        app.UseRewriter(options);
    }
}
```

`launchSettings.json`

```json
"launchUrl": "https://localhost:[port]/swagger",
```

## Register application

1. Create a new Azure AD (if you haven't got one)
1. Log in to <https://portal.azure.com/>
1. Add an app:
   - Name: BDSA2017.Lecture10.Web
   - Application Type: Web app / API
   - Sign-on URL: <https://localhost:[port]/>
1. Open and edit properties
   - Application ID: [copy]
   - App ID URI: <https://[tenantName].onmicrosoft.com/BDSA2017.Lecture10.Web>
1. Open and edit Reply URLs
   - Change to: <https://localhost:[port]/signin-oidc>
1. Open and edit Keys -> Passwords
   - Name: API
   - Expires: In 1 year
   - Value: [Save and copy]
1. Open and edit Required Permissions
   - Add Windows Azure Active Directory
   - Delegated permissions
     - Read directory data
     - Sign in and read user profile

### Configure Web API

`appsettings.json`

```json
{
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "[tenantName].onmicrosoft.com",
    "TenantId": "[tenantId]",
    "ClientId": "[clientId]",
    "CallbackPath": "/signin-oidc"
  }
}
```

`secrets.json`

```json
{
  "Authentication:AzureAd:ClientSecret": "[password]"
}
```

`launchSettings.json`

```json
{
  "iisSettings": {
    "iisExpress": {
      "applicationUrl": "http://localhost:53251/",
      "sslPort": 44323
    }
  },
  "profiles": {
    "IIS Express": {
      "launchUrl": "https://localhost:44323/"
    }
}
```

`Startup.cs`

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
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

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        app.UseAuthentication();
    }
}
```

`ValuesControllerTests.cs`

```csharp
public class ValuesControllerTests : Controller
{
    [Fact(DisplayName = "Controller has AuthorizeAttribute")]
    public void Controller_has_AuthorizeAttribute()
    {
        var type = typeof(CharactersController);

        var authorizeAttribute = type.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(AuthorizeAttribute));

        Assert.NotNull(authorizeAttribute);
    }
}
```

`ValuesController.cs`

```csharp
[Authorize]
[Route("api/[controller]")]
public class ValuesController : Controller
{
    ...
}
```
