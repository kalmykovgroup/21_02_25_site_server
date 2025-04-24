using Domain.Entities.ProductSpace;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.ProductSpace;

public class SellerOfferConf : AuditableEntityConf<SellerOffer>
{
    public override void Configure(EntityTypeBuilder<SellerOffer> entity)
    {
        base.Configure(entity);
        
        entity.ToTable("seller_offers");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasColumnName("id");

        entity.Property(e => e.ProductVariantId)
            .HasColumnName("product_variant_id")
            .IsRequired();

        entity.Property(e => e.SellerId)
            .HasColumnName("seller_id")
            .IsRequired();
        
        entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(255).IsRequired();

        entity.Property(e => e.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        entity.Property(e => e.StockQuantity)
            .HasColumnName("stock_quantity")
            .IsRequired();

        entity.Property(e => e.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);
 

        // Связи
        entity.HasOne(e => e.ProductVariant)
            .WithMany(pv => pv.SellerOffers) // или .WithMany(x => x.SellerOffers) если добавите коллекцию
            .HasForeignKey(e => e.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne(e => e.Seller)
            .WithMany(e => e.SellerOffers)
            .HasForeignKey(e => e.SellerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Уникальный индекс на (product_variant_id, seller_id),
        // чтобы один продавец не создал два оффера на тот же SKU.
        entity.HasIndex(e => new { e.ProductVariantId, e.SellerId })
            .IsUnique(true);
    
    }
}