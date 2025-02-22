 
using Domain.Entities.Common; 
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Domain.Entities.OrderSpace;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.OrderSpace
{ 
    public class OrderItemConf : AuditableEntityConf<OrderItem>
    {

        /// <summary>
        /// Настройка сущности OrderItemEntity.
        /// </summary>
        public override void Configure(EntityTypeBuilder<OrderItem> entity)
        {
            base.Configure(entity);

            // Связь с заказом
            entity.HasOne(e => e.Order)
                    .WithMany(o => o.Items)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Связь с товаром
                entity.HasOne(e => e.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasKey(e => e.Id);

                entity.ToTable("order_items");

                entity.Property(e => e.Id).IsRequired().HasColumnName("id");
                entity.Property(e => e.OrderId).IsRequired().HasColumnName("order_id");
                entity.Property(e => e.ProductId).IsRequired().HasColumnName("product_id");
                entity.Property(e => e.ProductName).IsRequired().HasColumnName("product_name").HasMaxLength(255);
                entity.Property(e => e.Quantity).IsRequired().HasColumnName("quantity");
                entity.Property(e => e.UnitPrice).IsRequired().HasColumnName("unit_price").HasPrecision(18, 2);
 
        }
    }


}
