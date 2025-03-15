using Domain.Entities.ProductSpace;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.ProductSpace;

public class SellerConf : AuditableEntityConf<Seller>
{
    public override void Configure(EntityTypeBuilder<Seller> entity)
    {
        base.Configure(entity);
        
        entity.ToTable("sellers");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasColumnName("id");

        entity.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        entity.Property(e => e.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);

        entity.Property(e => e.Rating)
            .HasColumnName("rating")
            .HasColumnType("decimal(3,2)")
            .HasDefaultValue(0);
 
 

    }
}