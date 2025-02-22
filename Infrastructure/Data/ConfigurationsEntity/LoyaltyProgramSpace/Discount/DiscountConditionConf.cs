

using Domain.Entities.LoyaltyProgramSpace.CouponSpace;
using Domain.Models.LoyaltyProgramSpace.Discount;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.Discount
{
    


    /// <summary>
    /// Отражает таблицу discount_conditions в БД. 
    /// Хранит настройки по конкретному условию (тип, оператор, параметры).
    /// </summary> 
    public class DiscountConditionConf : AuditableEntityConf<DiscountCondition>
    {
        public override void Configure(EntityTypeBuilder<DiscountCondition> entity)
        {
            base.Configure(entity);

            // Назначим DiscountCondition.Id первичным ключом
            entity.HasKey(dc => dc.Id);

            // Укажем, что Id генерируется приложением (или БД), 
            // если вы используете Guid.NewGuid() на уровне кода:
            entity.Property(dc => dc.Id)
                    .ValueGeneratedNever();

            // Foreign key на DiscountRule (discount_rule_id)
            // Обычно у нас есть класс DiscountRule с ICollection<DiscountCondition>.
            entity.HasOne<DiscountRule>()
                    .WithMany(dr => dr.DiscountConditions)
                    .HasForeignKey(dc => dc.DiscountRuleId)
                    .OnDelete(DeleteBehavior.Cascade);

            // Здесь мы указываем, что Operator хранится как строка (например, "And", "Or")
            entity.Property(dc => dc.Operator)
                    .HasConversion<string>()               // enum -> string
                    .HasColumnName("operator")
                    .HasMaxLength(5);                     // "AND" / "OR" - запас 5 символов

            // Аналогично ConditionType
            entity.Property(dc => dc.ConditionType)
                    .HasConversion<string>()               // enum -> string
                    .HasColumnName("condition_type")
                    .HasMaxLength(50);                    // "Category", "CartTotal", etc.

            // CategoryId, ProductId, etc. могут остаться как есть (Guid?).
            // Для decimal-полей (MinAmount, Threshold) можно уточнить precision:
            entity.Property(dc => dc.MinAmount)
                    .HasColumnName("min_amount")
                    .HasColumnType("decimal(18,2)");

            entity.Property(dc => dc.Threshold)
                    .HasColumnName("threshold")
                    .HasColumnType("decimal(18,2)");

            // Пример: Comparison поле (>, >=, etc.)
            entity.Property(dc => dc.Comparison)
                    .HasMaxLength(5);

            // Если хотим задать TableName через Fluent API (можно и так, дублируя [Table]):
            entity.ToTable("discount_conditions");

            // Остальные поля (MinQuantity, Quantity, MinUserOrders) можно не настраивать дополнительно,
            // если устраивают их дефолтные маппинги (int -> int).

            entity.Property(dc => dc.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("time");

            entity.Property(dc => dc.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("time");

            entity.Property(dc => dc.ApplicableDayOfWeek)
                    .HasColumnName("applicable_day_of_week");

            entity.Property(dc => dc.IsFirstOrder)
                    .HasColumnName("is_first_order");

            entity.Property(dc => dc.RequiredCouponId)
                    .HasColumnName("required_coupon_id");

            entity.Property(dc => dc.PaymentMethodId)
                    .HasColumnName("payment_method_id");

            entity.Property(dc => dc.CustomerGroupId)
                    .HasColumnName("customer_group_id");

            entity.HasOne(dc => dc.CustomerGroup)
                    .WithMany()
                    .HasForeignKey(dc => dc.CustomerGroupId)
                    .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(dc => dc.PaymentMethod)
                    .WithOne()
                    .HasForeignKey<DiscountCondition>(t => t.PaymentMethodId)
                    .OnDelete(DeleteBehavior.Cascade);
            
       }
    }



}
