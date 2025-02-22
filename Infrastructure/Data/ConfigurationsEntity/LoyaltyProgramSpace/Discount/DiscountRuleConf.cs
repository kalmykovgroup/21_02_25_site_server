 
using Domain.Models.LoyaltyProgramSpace.Discount;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.Discount
{
   

    /// <summary>
    /// Основное правило скидки. Определяет параметры скидки (тип, значение, период действия и т.д.).
    /// </summary>
    public class DiscountRuleConf : AuditableEntityConf<DiscountRule>
    {
        public override void Configure(EntityTypeBuilder<DiscountRule> entity)
        {
            base.Configure(entity);

          
                // Указываем имя таблицы:
                entity.ToTable("discount_rules");

                // Первичный ключ
                entity.HasKey(dr => dr.Id);

                // При необходимости сообщаем, что GUID генерируется на стороне приложения:
                entity.Property(dr => dr.Id)
                    .ValueGeneratedNever();
 

                // Поле Type -> столбец "type" (максимальная длина, например 50)
                entity.Property(dr => dr.DiscountRuleType)
                    .HasColumnName("type")
                    .HasConversion<int>();

                // Поле Value -> столбец "value" (decimal(18,2))
                entity.Property(dr => dr.Value)
                    .HasColumnName("value")
                    .HasColumnType("decimal(18,2)");

                // StartDate, EndDate, CreatedAt, UpdatedAt, MinOrderAmount и т.д.
                entity.Property(dr => dr.StartDate)
                    .HasColumnName("start_date");

                entity.Property(dr => dr.EndDate)
                    .HasColumnName("end_date");

                entity.Property(dr => dr.MinOrderAmount)
                    .HasColumnName("min_order_amount")
                    .HasColumnType("decimal(18,2)");

                entity.Property(dr => dr.MaxUsageCount)
                    .HasColumnName("max_usage_count");

                entity.Property(dr => dr.CurrentUsageCount)
                    .HasColumnName("current_usage_count");

                entity.Property(dr => dr.IsStackable)
                    .HasColumnName("is_stackable");

                entity.Property(dr => dr.Priority)
                    .HasColumnName("priority");
                 

                // Настройка связей:
                // Предположим, DiscountCondition, DiscountBundle, Coupon, DiscountUsage
                // имеют FK: discount_rule_id, и навигацию public virtual DiscountRule? DiscountRule { get; set; }.

                // One-to-Many: DiscountRule -> DiscountCondition
                // Если хотите каскадное удаление при удалении DiscountRule:
                entity.HasMany(dr => dr.DiscountConditions)
                       .WithOne(dc => dc.DiscountRule)          // свойство в DiscountCondition
                       .HasForeignKey(dc => dc.DiscountRuleId)
                       .OnDelete(DeleteBehavior.Cascade);

                // One-to-Many: DiscountRule -> DiscountBundle
                entity.HasMany(dr => dr.DiscountBundles)
                       .WithOne(db => db.DiscountRule!)
                       .HasForeignKey(db => db.DiscountRuleId)
                       .OnDelete(DeleteBehavior.Cascade);

                // One-to-Many: DiscountRule -> Coupon
                entity.HasMany(dr => dr.Coupons)
                       .WithOne(c => c.DiscountRule!)
                       .HasForeignKey(c => c.DiscountRuleId)
                       .OnDelete(DeleteBehavior.Cascade);

                // One-to-Many: DiscountRule -> DiscountUsage
                entity.HasMany(dr => dr.DiscountUsages)
                       .WithOne(du => du.DiscountRule!)
                       .HasForeignKey(du => du.DiscountRuleId)
                       .OnDelete(DeleteBehavior.Cascade);

                entity.Property(dr => dr.IsExclusive)
                        .HasColumnName("is_exclusive")
                        .HasDefaultValue(false);

                // Если вы НЕ хотите каскадного удаления, замените .OnDelete(DeleteBehavior.Cascade)
                // на DeleteBehavior.Restrict или SetNull.
          
        }
    }


}
