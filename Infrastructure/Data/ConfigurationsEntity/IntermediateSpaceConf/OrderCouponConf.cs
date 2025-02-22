using Domain.Entities.IntermediateSpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.IntermediateSpaceConf
{
    public class OrderCouponConf : IEntityTypeConfiguration<OrderCoupon>
    { 
        public void Configure(EntityTypeBuilder<OrderCoupon> builder)
        {
            builder.ToTable("order_coupons");

            // Составной ключ
            builder.HasKey(oc => new { oc.OrderId, oc.CouponId });

            // Связь с заказом
            builder.HasOne(oc => oc.Order)
                .WithMany(o => o.OrderCoupons)
                .HasForeignKey(oc => oc.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с купоном
            builder.HasOne(oc => oc.Coupon)
                .WithMany(c => c.OrderCoupons)
                .HasForeignKey(oc => oc.CouponId)
                .OnDelete(DeleteBehavior.Cascade);

            // Настройка DiscountAmount
            builder.Property(oc => oc.DiscountAmount)
                .IsRequired()
                .HasPrecision(18, 2);
        }
    }
}
