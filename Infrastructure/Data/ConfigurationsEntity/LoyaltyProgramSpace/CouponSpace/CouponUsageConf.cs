using Domain.Entities.LoyaltyProgramSpace.CouponSpace;
using Domain.Models.LoyaltyProgramSpace;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.CouponSpace
{
    public class CouponUsageConf : AuditableEntityConf<CouponUsage>
    {
        public override void Configure(EntityTypeBuilder<CouponUsage> entity)
        {
            base.Configure(entity);

            
            // 1) Имя таблицы
            entity.ToTable("coupon_usages");

            // 2) Первичный ключ
            entity.HasKey(cu => cu.Id);

            // 3) Настройка полей
            entity.Property(cu => cu.Id).ValueGeneratedNever();
            entity.Property(cu => cu.CouponId).HasColumnName("coupon_id");
            entity.Property(cu => cu.CustomerId).HasColumnName("customer_id");
            entity.Property(cu => cu.UsageDate).HasColumnName("usage_date");
            entity.Property(cu => cu.IsSuccessful).HasColumnName("is_successful");
            entity.Property(cu => cu.ErrorMessage)
                    .HasColumnName("error_message")
                    .HasMaxLength(500);

            // 4) Связь с Coupon
            entity.HasOne(cu => cu.Coupon)
                    .WithMany(c => c.CouponUsages)
                    .HasForeignKey(cu => cu.CouponId)
                    .OnDelete(DeleteBehavior.Cascade);

            // 5) Связь с UserEntity
            entity.HasOne(cu => cu.Customer)
                    .WithMany(u => u.CouponUsages)
                    .HasForeignKey(cu => cu.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
