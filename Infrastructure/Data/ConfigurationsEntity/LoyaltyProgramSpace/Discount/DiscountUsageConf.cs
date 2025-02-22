

using Domain.Models.LoyaltyProgramSpace.Discount;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.Discount
{


    /// <summary>
    /// Отражает факт использования скидки конкретным пользователем в рамках определённого заказа.
    /// </summary>
    public class DiscountUsageConf : AuditableEntityConf<DiscountUsage>
    {
        public override void Configure(EntityTypeBuilder<DiscountUsage> entity)
        {
            base.Configure(entity);
 
            // 1) Имя таблицы:
            entity.ToTable("discount_usages");

            // 2) Первичный ключ:
            entity.HasKey(du => du.Id);

            // Если GUID генерируется на стороне приложения, используем ValueGeneratedNever():
            entity.Property(du => du.Id)
                    .ValueGeneratedNever();

            // 3) Настраиваем колонки:
            entity.Property(du => du.DiscountRuleId)
                    .HasColumnName("discount_rule_id");

            entity.Property(du => du.CustomerId)
                    .HasColumnName("user_id");

            entity.Property(du => du.OrderId)
                    .HasColumnName("order_id");

            entity.Property(du => du.UsageDate)
                    .HasColumnName("usage_date");

            entity.Property(du => du.AppliedAmount)
                    .HasColumnName("applied_amount")
                    .HasColumnType("decimal(18,2)");

            // 4) Связи (FK) с DiscountRule, UserCustomer, Order.
            // По желанию можно назначить Cascade / Restrict / SetNull при удалении.

            // DiscountUsage -> DiscountRule
            entity.HasOne(du => du.DiscountRule)
                    .WithMany(dr => dr.DiscountUsages)  // Навигация в DiscountRule
                    .HasForeignKey(du => du.DiscountRuleId)
                    .OnDelete(DeleteBehavior.Cascade);   // или Restrict/SetNull

            // DiscountUsage -> UserCustomer
            entity.HasOne(du => du.Customer)
                    .WithMany(u => u.DiscountUsages)     // Навигация в UserCustomer
                    .HasForeignKey(du => du.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

            // DiscountUsage -> Order
            entity.HasOne(du => du.Order)
                    .WithMany(o => o.DiscountUsages)     // Навигация в Order
                    .HasForeignKey(du => du.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            
        }
    }

}
