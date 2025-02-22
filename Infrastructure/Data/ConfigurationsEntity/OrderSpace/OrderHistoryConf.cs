using Domain.Entities.Common;
using Domain.Entities.StatusesSpace;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Domain.Entities.OrderSpace;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.OrderSpace
{
    [Table("order_histories")]
    public class OrderHistoryConf : AuditableEntityConf<OrderHistory>
    {

        /// <summary>
        /// Настройка сущности OrderHistory.
        /// </summary>
        public override void Configure(EntityTypeBuilder<OrderHistory> entity)
        {
            base.Configure(entity);

            // Связь с заказом
            entity.HasOne(e => e.Order)
                    .WithMany(o => o.OrderHistories)
                    .HasForeignKey(e => e.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Связь со статусом заказа
                entity.HasOne(e => e.OrderStatus)
                    .WithMany()
                    .HasForeignKey(e => e.OrderStatusId)
                    .OnDelete(DeleteBehavior.Restrict);


                entity.ToTable("order_histories");

                entity.HasKey(dr => dr.Id);

                entity.Property(e => e.Id).HasColumnName("id");


                // Настройка свойства Comments
                entity.Property(e => e.Comments).IsRequired(false)
                .HasMaxLength(1000)
                .HasColumnName("comments");
                 
                entity.Property(e => e.OrderId).IsRequired().HasColumnName("order_id");

                entity.Property(e => e.OrderId).IsRequired().HasColumnName("order_status_id");
           
        }
    }

}
