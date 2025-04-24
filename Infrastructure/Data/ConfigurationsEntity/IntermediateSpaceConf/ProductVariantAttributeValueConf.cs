using Domain.Entities.IntermediateSpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.IntermediateSpaceConf;

public class ProductVariantAttributeValueConf : IEntityTypeConfiguration<ProductVariantAttributeValue>
{
    public void Configure(EntityTypeBuilder<ProductVariantAttributeValue> entity)
    {
        entity.ToTable("product_variant_attribute_values");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasColumnName("id");

        entity.Property(e => e.ProductVariantId)
            .HasColumnName("product_variant_id")
            .IsRequired();

        entity.Property(e => e.CategoryAttributeId)
            .HasColumnName("category_attribute_id")
            .IsRequired();

        entity.Property(e => e.CategoryAttributeValueId)
            .HasColumnName("category_attribute_value_id")
            .IsRequired();

        // Связи
        entity.HasOne(e => e.ProductVariant)
            .WithMany(e => e.ProductVariantAttributeValues)
            .HasForeignKey(e => e.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.CategoryAttribute)
            .WithMany()
            .HasForeignKey(e => e.CategoryAttributeId);

        entity.HasOne(e => e.CategoryAttributeValue)
            .WithMany()
            .HasForeignKey(e => e.CategoryAttributeValueId);

        // Уникальный индекс на (product_variant_id, category_attribute_id),
        // чтобы не было дубликатов "SKU X + Color" дважды.
        entity.HasIndex(e => new { e.ProductVariantId, e.CategoryAttributeId })
            .IsUnique(true);
    }
}