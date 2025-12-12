using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DocumentIngestion.Api.Infrastructure.SwaggerConfig;
public static class SwaggerConfig
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "DocumentIngestion.Api", Version = "v1" });
            c.TagActionsBy(_ => ["Invoices"]);
            c.DocInclusionPredicate((_, _) => true);

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}
