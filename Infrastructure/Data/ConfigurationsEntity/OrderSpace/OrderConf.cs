using Domain.Entities._Notifications;
using Domain.Entities.OrderSpace;
using Domain.Entities.PaymentSpace;
using Domain.Entities.StatusesSpace.Heirs;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.OrderSpace
{
    public class OrderConf : AuditableEntityConf<Order>
    {

        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.ToTable("orders");

            builder.Property(o => o.TotalAmount)
                   .HasComputedColumnSql("\"sub_total\" - \"total_discount\" + \"tax_amount\"", stored: true)
                   .HasColumnName("total_amount")
                   .HasPrecision(18, 2);


                builder.Property(o => o.OrderNumber).HasColumnName("order_number").IsRequired().HasMaxLength(50);

                builder.Property(o => o.SubTotal).HasColumnName("sub_total").IsRequired().HasPrecision(18, 2); 
                builder.Property(o => o.TotalDiscount).HasColumnName("total_discount").IsRequired().HasPrecision(18, 2); 
                builder.Property(o => o.TaxAmount).HasColumnName("tax_amount").IsRequired().HasPrecision(18, 2);  


                builder.Property(o => o.PaymentDetailsId).HasColumnName("payment_details_id");
                builder.Property(o => o.ShippingDetailsId).HasColumnName("shipping_details_id");
                builder.Property(o => o.CustomerId).HasColumnName("customer_id");
                builder.Property(o => o.OrderStatusId).HasColumnName("order_status_id");

                builder.HasOne(o => o.PaymentDetails)
                .WithOne(pd => pd.Order)
                .HasForeignKey<PaymentDetails>(pd => pd.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(o => o.ShippingDetails)
                .WithOne(sd => sd.Order)
                .HasForeignKey<ShippingDetails>(sd => sd.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


                // Связь с клиентом
                builder.HasOne(e => e.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Связь со статусом заказа
                builder.HasOne(e => e.OrderStatus)
                    .WithOne()
                    .HasForeignKey<Order>(e => e.OrderStatusId)
                    .OnDelete(DeleteBehavior.Restrict);

                
           
        }
    }
}
