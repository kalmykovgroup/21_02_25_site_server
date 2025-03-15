using Domain.Entities.ProductSpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.ProductSpace;

public class ProductVariantConf : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> entity)
    {
        entity.ToTable("product_variants");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasColumnName("id");

        entity.Property(e => e.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        entity.Property(e => e.Sku)
            .HasColumnName("sku")
            .HasMaxLength(255)
            .IsRequired();

        entity.Property(e => e.Barcode)
            .HasColumnName("barcode")
            .HasMaxLength(255);

        entity.Property(e => e.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);
 

        // Уникальные индексы на SKU и Barcode.
        entity.HasIndex(e => e.Sku)
            .IsUnique(true);

        entity.HasIndex(e => e.Barcode)
            .IsUnique(true)
            .HasFilter("[barcode IS NOT NULL");  
 
    }
}