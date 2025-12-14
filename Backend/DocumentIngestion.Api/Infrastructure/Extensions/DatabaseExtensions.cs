using DocumentIngestion.Api.Invoices.Context;
using Microsoft.EntityFrameworkCore;

namespace DocumentIngestion.Api.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<InvoiceDbContext>>();
        
        try
        {
            var dbContext = services.GetRequiredService<InvoiceDbContext>();
            
            logger.LogInformation("Ensuring database is created...");
            
            // EnsureCreated creates the database if it doesn't exist
            // This is simpler than migrations for development
            var created = dbContext.Database.EnsureCreated();
            
            if (created)
            {
                logger.LogInformation("Database created successfully.");
            }
            else
            {
                logger.LogInformation("Database already exists.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while creating the database.");
            throw;
        }
    }
}
