using Micro.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.Catalog.Infrastructure.Persistence.Application.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .HasMaxLength(511)
            .IsRequired();

        builder
            .HasMany(t => t.Categories)
            .WithMany(t => t.Products);
    }
}