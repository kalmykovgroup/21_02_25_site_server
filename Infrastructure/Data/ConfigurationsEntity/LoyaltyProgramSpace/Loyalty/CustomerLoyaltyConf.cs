using Domain.Models.LoyaltyProgramSpace.Discount;
using Domain.Models.LoyaltyProgramSpace.Loyalty;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.Loyalty
{ 

    /// <summary>
    /// Отражает участие пользователя в программе лояльности и хранит текущие баллы и уровень.
    /// </summary>
    public class CustomerLoyaltyConf : AuditableEntityConf<CustomerLoyalty>
    {
        public override void Configure(EntityTypeBuilder<CustomerLoyalty> entity)
        {
            base.Configure(entity);
 
            // 1) Имя таблицы
            entity.ToTable("user_loyalty");

            // 2) Первичный ключ
            entity.HasKey(ul => ul.Id);

            // Предполагаем, что Id генерируется в коде (Guid.NewGuid())
            entity.Property(ul => ul.Id)
                    .ValueGeneratedNever();

            // 3) Настраиваем поля (колонки)
            entity.Property(ul => ul.CustomerId)
                    .HasColumnName("user_id");

            entity.Property(ul => ul.LoyaltyProgramId)
                    .HasColumnName("loyalty_program_id");

            entity.Property(ul => ul.CurrentTierId)
                    .HasColumnName("current_tier_id");

            entity.Property(ul => ul.TotalPoints)
                    .HasColumnName("total_points");

            entity.Property(ul => ul.Active)
                    .HasColumnName("active");
                 

            // 4) Связи

            // CustomerLoyalty -> UserCustomer
            // Many-to-One (многие записи CustomerLoyalty могут ссылаться на одного UserCustomer).
            // Предполагается, что UserCustomer имеет коллекцию DiscountUsages, CustomerLoyaltyRecords, и т.п.
            entity.HasOne(ul => ul.Customer)
                    .WithMany(c => c.CustomerLoyaltyRecords)  // Навигация у UserCustomer (ICollection<CustomerLoyalty>)
                    .HasForeignKey(ul => ul.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);     // или Restrict/SetNull в зависимости от логики

            // CustomerLoyalty -> LoyaltyProgram
            entity.HasOne(ul => ul.LoyaltyProgram)
                    .WithMany(lp => lp.UserLoyaltyRecords)
                    .HasForeignKey(ul => ul.LoyaltyProgramId)
                    .OnDelete(DeleteBehavior.Cascade);

            // CustomerLoyalty -> LoyaltyTier (CurrentTier)
            // Один уровень (LoyaltyTier) может быть назначен многим пользователям (CustomerLoyalty).
            // Поле current_tier_id может быть null -> Optional relationship
            entity.HasOne(ul => ul.CurrentTier)
                    .WithMany(lt => lt.UserLoyaltyRecords)
                    .HasForeignKey(ul => ul.CurrentTierId)
                    .OnDelete(DeleteBehavior.SetNull);
            // При удалении LoyaltyTier ссылка в CustomerLoyalty обнуляется
          
        }
    }


}
