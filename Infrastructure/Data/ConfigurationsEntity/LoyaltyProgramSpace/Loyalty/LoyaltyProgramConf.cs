

using Domain.Entities.LoyaltyProgramSpace.Loyalty; 
using Domain.Models.LoyaltyProgramSpace.Loyalty;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ConfigurationsEntity.LoyaltyProgramSpace.Loyalty
{ 
    /// <summary>
    /// Программа лояльности, в рамках которой пользователи могут накапливать баллы и повышать уровни.
    /// </summary>
    public class LoyaltyProgramConf : SeoEntityConf<LoyaltyProgram>
    {
        public override void Configure(EntityTypeBuilder<LoyaltyProgram> entity)
        {
            base.Configure(entity);
     
            // 1) Имя таблицы
            entity.ToTable("loyalty_programs");

            // 2) Первичный ключ
            entity.HasKey(lp => lp.Id);

            // Предполагаем, что Id генерируется приложением (Guid.NewGuid()):
            entity.Property(lp => lp.Id)
                    .ValueGeneratedNever();
                 

            entity.Property(lp => lp.DefaultPointsPerDollar)
                    .HasColumnName("default_points_per_dollar");

            entity.Property(lp => lp.PointsToCurrencyRatio)
                    .HasColumnName("points_to_currency_ratio")
                    .HasColumnType("decimal(18,4)");

            // 4) Связи (One-to-Many: LoyaltyProgram -> LoyaltyTier, LoyaltyProgram -> CustomerLoyalty).
            // Предположим, что у LoyaltyTier и CustomerLoyalty есть поля
            // loyalty_program_id, ссылающиеся на LoyaltyProgram.

            entity.HasMany(lp => lp.LoyaltyTiers)
                    .WithOne(lt => lt.LoyaltyProgram!)
                    .HasForeignKey(lt => lt.LoyaltyProgramId)
                    .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(lp => lp.UserLoyaltyRecords)
                    .WithOne(ul => ul.LoyaltyProgram!)
                    .HasForeignKey(ul => ul.LoyaltyProgramId)
                    .OnDelete(DeleteBehavior.Cascade);
           
        }
    }

}
