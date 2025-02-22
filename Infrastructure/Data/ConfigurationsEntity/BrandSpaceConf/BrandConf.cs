using Domain.Entities.AnalyticsSpace;
using Domain.Entities.BrandSpace; 
using Infrastructure.Data.ConfigurationsEntity.Common; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.BrandSpaceConf
{
    public class BrandConf : SeoEntityConf<Brand>
    {
        public override void Configure(EntityTypeBuilder<Brand> entity)
        {
            base.Configure(entity);

            entity.ToTable("brand");
 
            entity.Property(t => t.Name).IsRequired().HasColumnName("name").HasMaxLength(255);

            // Связь с предпочтениями пользователей
            entity.HasMany(b => b.CustomerPreferenceBrands)
                .WithOne(cpb => cpb.Brand)
                .HasForeignKey(cpb => cpb.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с продуктами
            entity.HasMany(b => b.Products)
                .WithOne(p => p.Brand)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            // Связь с поставщиками
            entity.HasMany(b => b.SupplierBrands)
                .WithOne(sb => sb.Brand)
                .HasForeignKey(sb => sb.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            // Настройка полей
            entity.Property(b => b.LogoUrl)
                .HasMaxLength(2048);

            entity.Property(b => b.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Индекс на поле IsActive
            entity.HasIndex(b => b.IsActive)
                .HasDatabaseName("IX_Brand_IsActive");
         
        }
    }
}
