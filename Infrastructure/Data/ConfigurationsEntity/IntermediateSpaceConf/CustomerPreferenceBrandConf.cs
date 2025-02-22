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
    public class CustomerPreferenceBrandConf : IEntityTypeConfiguration<CustomerPreferenceBrand>
    {
       /// <summary>
        /// Настройка сущности CustomerPreferenceBrand.
        /// Определяет связи с предпочтениями клиента и брендами.
        /// </summary> 
        public void Configure(EntityTypeBuilder<CustomerPreferenceBrand> builder)
        {

            builder.ToTable("customer_preference_brands");

            // Составной ключ
            builder.HasKey(e => new { e.CustomerPreferenceId, e.BrandId });

            builder.Property(e => e.BrandId).HasColumnName("brand_id");
            builder.Property(e => e.CustomerPreferenceId).HasColumnName("customer_preference_id");

            // Связь с предпочтением клиента
            builder.HasOne(e => e.CustomerPreference)
                .WithMany(cp => cp.FavoriteBrandLinks)
                .HasForeignKey(e => e.CustomerPreferenceId);

            // Связь с брендом
            builder.HasOne(e => e.Brand)
                .WithMany(b => b.CustomerPreferenceBrands)
                .HasForeignKey(e => e.BrandId);
        }
    }
}
