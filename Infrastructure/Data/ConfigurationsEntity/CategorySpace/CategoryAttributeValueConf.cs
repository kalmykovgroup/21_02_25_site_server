using Domain.Entities.CategorySpace;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.CategorySpace;

public class CategoryAttributeValueConf: AuditableEntityConf<CategoryAttributeValue>
{
    public override void Configure(EntityTypeBuilder<CategoryAttributeValue> entity)
    {
        base.Configure(entity);
        
        entity.ToTable("category_attribute_values");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasColumnName("id");

        entity.Property(e => e.CategoryAttributeId)
            .HasColumnName("category_attribute_id")
            .IsRequired();

        entity.Property(e => e.Value)
            .HasColumnName("value")
            .HasMaxLength(255)
            .IsRequired();
 

        // Связь "многие к одному": CategoryAttributeValue -> CategoryAttribute
        entity.HasOne(e => e.CategoryAttribute)
            .WithMany(e => e.CategoryAttributeValues)
            .HasForeignKey(e => e.CategoryAttributeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Уникальный индекс на (category_attribute_id, value)
        entity.HasIndex(e => new { e.CategoryAttributeId, e.Value })
            .IsUnique(true);
    }
}