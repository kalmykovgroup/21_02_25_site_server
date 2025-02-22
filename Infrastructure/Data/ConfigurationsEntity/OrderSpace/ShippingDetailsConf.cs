using Domain.Entities.Common;
using Domain.Entities.StatusesSpace;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.AddressesSpace;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Domain.Entities.OrderSpace;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.OrderSpace
{
    /// <summary>
    /// Сущность, представляющая информацию о доставке.
    /// Хранит данные о получателе, адресе доставки, статусе,
    /// а также временные метки, такие как дата отправки и доставки.
    /// </summary> 
    public class ShippingDetailsConf : AuditableEntityConf<ShippingDetails>
    {

        /// <summary>
        /// Настройка сущности ShippingDetails.
        /// Определяет связи и дополнительные ограничения.
        /// </summary>
        public override void Configure(EntityTypeBuilder<ShippingDetails> entity)
        {
            base.Configure(entity);

            // Связь с заказом
            entity.HasOne(e => e.Order)
                    .WithOne(o => o.ShippingDetails)
                    .HasForeignKey<ShippingDetails>(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Связь с адресом доставки
                entity.HasOne(e => e.Address)
                    .WithOne()
                    .HasForeignKey<ShippingDetails>(e => e.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Связь со статусом доставки
                entity.HasOne(e => e.ShippingStatus)
                    .WithOne()
                    .HasForeignKey<ShippingDetails>(e => e.ShippingStatusId)
                    .OnDelete(DeleteBehavior.Restrict);


                entity.ToTable("shipping_details");

                entity.HasKey(dr => dr.Id);
               
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrderId).IsRequired().HasColumnName("order_id");
                entity.Property(e => e.RecipientName).IsRequired().HasColumnName("recipient_name").HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).IsRequired().HasColumnName("phone_number").HasMaxLength(20);
                entity.Property(e => e.AddressId).IsRequired().HasColumnName("address_id");
                entity.Property(e => e.ShippingStatusId).IsRequired().HasColumnName("shipping_status_id");
                entity.Property(e => e.ShippedAt).IsRequired().HasColumnName("shipped_at");
                entity.Property(e => e.DeliveredAt).IsRequired().HasColumnName("delivered_at");

 
        }
    }

}
