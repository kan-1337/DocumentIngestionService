using Microsoft.AspNetCore.Builder;

namespace Shared.Common.Extensions;

public static class OpenApiExtensions
{
    public static TEndpointBuilder WithParameterDescriptions<TEndpointBuilder>(
        this TEndpointBuilder builder,
        params (string Name, string Description)[] paramDescriptions)
        where TEndpointBuilder : IEndpointConventionBuilder
    {
        foreach (var (name, desc) in paramDescriptions)
        {
            builder.WithOpenApi(op =>
            {
                var param = op.Parameters?.FirstOrDefault(p => p.Name == name);
                if (param is not null) param.Description = desc;
                return op;
            });
        }
        return builder;
    }
}

