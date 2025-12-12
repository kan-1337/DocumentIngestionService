using DocumentIngestion.Api.ExternalSystems;
using DocumentIngestion.Api.Invoices.Repositories;
using DocumentIngestion.Api.Invoices.Services;

namespace DocumentIngestion.Api.Infrastructure.DependencyInjection;
public static class InvoiceServiceConfig
{
    public static IServiceCollection AddInvoiceServicesAndRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IInvoiceRepository, InMemoryInvoiceService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddSingleton<IExternalSystemClient, FakeExternalSystemClient>();
        return services;
    }
}
