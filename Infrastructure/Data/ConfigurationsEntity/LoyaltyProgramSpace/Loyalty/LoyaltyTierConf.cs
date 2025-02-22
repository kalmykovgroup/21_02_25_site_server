
 
using Domain.Models.LoyaltyProgramSpace.Loyalty;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.Loyalty
{

    /// <summary>
    /// Уровень в программе лояльности (например, Bronze, Silver, Gold).
    /// </summary>
    public class LoyaltyTierConf : AuditableEntityConf<LoyaltyTier> 
    {
        public override void Configure(EntityTypeBuilder<LoyaltyTier> entity)
        {
            base.Configure(entity);
 
            // 1) Имя таблицы
            entity.ToTable("loyalty_tiers");

            // 2) Первичный ключ
            entity.HasKey(lt => lt.Id);

            // Если GUID генерируется в приложении:
            entity.Property(lt => lt.Id)
                    .ValueGeneratedNever();

            // 3) Настраиваем поля (колонки)
            entity.Property(lt => lt.LoyaltyProgramId)
                    .HasColumnName("loyalty_program_id");
                 

            entity.Property(lt => lt.MinPoints)
                    .HasColumnName("min_points");

            entity.Property(lt => lt.DiscountPercentage)
                    .HasColumnName("discount_percentage")
                    .HasColumnType("decimal(5,2)");

            entity.Property(lt => lt.PointsMultiplier)
                    .HasColumnName("points_multiplier")
                    .HasColumnType("decimal(5,2)");

            // 4) Связь One-to-Many: LoyaltyProgram -> LoyaltyTier
            entity.HasOne(lt => lt.LoyaltyProgram)
                    .WithMany(lp => lp.LoyaltyTiers)
                    .HasForeignKey(lt => lt.LoyaltyProgramId)
                    .OnDelete(DeleteBehavior.Cascade);

            // 5) Связь One-to-Many: LoyaltyTier -> CustomerLoyalty
            // Предполагается, что CustomerLoyalty имеет поле CurrentTierId -> LoyaltyTier.Id.
            // Если хотите, чтобы удаление Tier обнуляло CurrentTier у пользователей, используйте SetNull:
            entity.HasMany(lt => lt.UserLoyaltyRecords)
                    .WithOne(ul => ul.CurrentTier!)
                    .HasForeignKey(ul => ul.CurrentTierId)
                    .OnDelete(DeleteBehavior.SetNull);
           
        }
    }

}
