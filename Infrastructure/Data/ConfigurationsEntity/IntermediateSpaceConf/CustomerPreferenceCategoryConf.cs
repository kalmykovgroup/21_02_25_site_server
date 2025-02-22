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
    public class CustomerPreferenceCategoryConf : IEntityTypeConfiguration<CustomerPreferenceCategory>
    {
        /// <summary>
        /// Настройка сущности CustomerPreferenceCategory.
        /// Определяет связи с предпочтениями клиента и категориями.
        /// </summary> 
        public void Configure(EntityTypeBuilder<CustomerPreferenceCategory> entity)
        {
            entity.ToTable("customer_preference_categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CustomerPreferenceId).HasColumnName("customer_preference_id");

            // Настройка составного ключа
            entity.HasKey(e => new { e.CustomerPreferenceId, e.CategoryId });

            // Связь с предпочтением клиента
            entity.HasOne(e => e.CustomerPreference)
                .WithMany(cp => cp.FavoriteCategoryLinks)
                .HasForeignKey(e => e.CustomerPreferenceId);

            // Связь с категорией
            entity.HasOne(e => e.Category)
                .WithMany(c => c.CustomerPreferenceCategories)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}
