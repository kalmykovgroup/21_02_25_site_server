 
using Domain.Entities.CategorySpace;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.CategorySpace;

public class CategoryAttributeConf: AuditableEntityConf<CategoryAttribute>
{
    public override void Configure(EntityTypeBuilder<CategoryAttribute> entity)
    {
        base.Configure(entity);
       
            entity.ToTable("category_attributes"); // snake_case имя таблицы

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.CategoryId)
                .HasColumnName("category_id")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.IsFilterable)
                .HasColumnName("is_filterable")
                .HasDefaultValue(true);

            entity.Property(e => e.IsVariation)
                .HasColumnName("is_variation")
                .HasDefaultValue(false);

            entity.Property(e => e.Position)
                .HasColumnName("position")
                .HasDefaultValue(1);
            
            entity.HasOne(e => e.Category)
                .WithMany(e => e.CategoryAttributes)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
 
     
    }
}