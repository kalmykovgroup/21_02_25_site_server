using Domain.Entities.UserSpace; 
using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.IntermediateSpace;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Domain.Entities.AddressesSpace;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.AnalyticsSpace
{
    /// <summary>
    /// Предпочтения клиента для персонализации взаимодействия
    /// </summary> 
    public class CustomerPreferenceConf : AuditableEntityConf<CustomerPreference>
    {

        /// <summary>
        /// Настройка сущности CustomerPreference
        /// </summary>
        /// 
        public override void Configure(EntityTypeBuilder<CustomerPreference> builder)
        {
            base.Configure(builder);

            builder.ToTable("customer_preference");

            // Связь с категориями через посредник
            builder.HasMany(e => e.FavoriteCategoryLinks)
                .WithOne(cpc => cpc.CustomerPreference)
                .HasForeignKey(cpc => cpc.CustomerPreferenceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с брендами через посредник
            builder.HasMany(e => e.FavoriteBrandLinks)
                .WithOne(cpb => cpb.CustomerPreference)
                .HasForeignKey(cpb => cpb.CustomerPreferenceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Настройка поля NotificationFrequency
            builder.Property(e => e.NotificationFrequency)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValue("Daily");

            // Настройка поля ReceiveDiscountNotifications
            builder.Property(e => e.ReceiveDiscountNotifications)
                .IsRequired()
                .HasDefaultValue(true);

            // Настройка поля ReceiveNewArrivalNotifications
            builder.Property(e => e.ReceiveNewArrivalNotifications)
                .IsRequired()
                .HasDefaultValue(true);
                     
        }
    }

}
