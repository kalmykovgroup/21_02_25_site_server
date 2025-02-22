


using Domain.Models.LoyaltyProgramSpace;
using Domain.Models.LoyaltyProgramSpace.Bundle;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.CouponSpace
{
    /// <summary>
    /// Купон, который предоставляет определённую скидку при выполнении условий.
    /// </summary>
    public class CouponConf : AuditableEntityConf<Coupon>
    {
        public override void Configure(EntityTypeBuilder<Coupon> entity)
        {
            base.Configure(entity);

           
            // 1) Указываем имя таблицы:
            entity.ToTable("coupons");

            // 2) Первичный ключ:
            entity.HasKey(c => c.Id);

            // Если GUID генерируется на стороне приложения:
            entity.Property(c => c.Id)
                    .ValueGeneratedNever();

            // 3) Настраиваем поля:
            entity.Property(c => c.Code)
                    .HasColumnName("code")
                    .HasMaxLength(50);

            entity.Property(c => c.ExpirationDate)
                    .HasColumnName("expiration_date");

            entity.Property(c => c.MaxUses)
                    .HasColumnName("max_uses");

            entity.Property(c => c.CurrentUses)
                    .HasColumnName("current_uses");

            entity.Property(c => c.IsSingleUsePerUser)
                    .HasColumnName("is_single_use_per_user");

            entity.Property(c => c.DiscountRuleId)
                    .HasColumnName("discount_rule_id");


            // 4) Связь с DiscountRule (FK discount_rule_id)
            entity.HasOne(c => c.DiscountRule)
                    .WithMany(dr => dr.Coupons)            // Навигация в DiscountRule
                    .HasForeignKey(c => c.DiscountRuleId)
                    .OnDelete(DeleteBehavior.Cascade);
            
        }
    }

}
