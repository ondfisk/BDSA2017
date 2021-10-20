using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace BDSA2017.Lecture10.Web.Models
{
    public class LowerCaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.ToDictionary(
                entry => string.Join('/', entry.Key.Split('/').Select(x => x.ToLower())),
                entry => entry.Value);

            swaggerDoc.Paths = new OpenApiPaths();

            foreach ((string key, OpenApiPathItem value) in paths)
            {
                foreach (var param in value.Operations.SelectMany(o => o.Value.Parameters))
                {
                    param.Name = param.Name.ToLower();
                }

                swaggerDoc.Paths.Add(key, value);
            }
        }
    }
}
