using DocumentIngestion.Api.Invoices.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentIngestion.Api.Invoices.Context;

public class InvoiceDbContext : DbContext
{
    public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options) { }

    public DbSet<Invoice> Invoices => Set<Invoice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>(b =>
        {
            b.OwnsMany(i => i.Lines, lines =>
            {
                lines.ToTable("InvoiceLines");
                lines.WithOwner().HasForeignKey("InvoiceId");
                lines.HasKey(x => x.Id);
            });

            b.Navigation(i => i.Lines)
             .UsePropertyAccessMode(PropertyAccessMode.Field);

            b.Metadata.FindNavigation(nameof(Invoice.Lines))!
             .SetField("_lines");
        });
    }
}
