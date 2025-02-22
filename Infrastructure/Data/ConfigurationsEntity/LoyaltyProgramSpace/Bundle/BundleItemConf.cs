

using Domain.Entities.InventorySpace;
using Domain.Models.LoyaltyProgramSpace.Bundle;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.Bundle
{
    /// <summary>
    /// Элемент набора (bundle) товаров для групповой скидки.
    /// Связывает конкретный товар (ProductEntity) с бандл-скидкой (DiscountBundle).
    /// </summary>
    public class BundleItemConf : AuditableEntityConf<BundleItem>
    {
        public override void Configure(EntityTypeBuilder<BundleItem> entity)
        {
            base.Configure(entity);

          
            // 1) Имя таблицы
            entity.ToTable("bundle_items");

            // 2) Первичный ключ
            entity.HasKey(bi => bi.Id);

            // Предполагаем, что Id (Guid) генерируется в коде
            entity.Property(bi => bi.Id)
                    .ValueGeneratedNever();

            // 3) Настройка полей
            entity.Property(bi => bi.DiscountBundleId)
                    .HasColumnName("discount_bundle_id");

            entity.Property(bi => bi.ProductId)
                    .HasColumnName("product_id");

            entity.Property(bi => bi.Quantity)
                    .HasColumnName("quantity");

            // 4) Связи

            // Связь BundleItem -> DiscountBundle
            entity.HasOne(bi => bi.DiscountBundle)
                    .WithMany(db => db.BundleItems)   // Навигация в DiscountBundle
                    .HasForeignKey(bi => bi.DiscountBundleId)
                    .OnDelete(DeleteBehavior.Cascade);

            // Связь BundleItem -> ProductEntity
            entity.HasOne(bi => bi.Product)
                    .WithMany(p => p.BundleItems)     // Навигация в ProductEntity
                    .HasForeignKey(bi => bi.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            
        }
    }



}
