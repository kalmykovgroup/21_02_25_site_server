using Domain.Entities.Common;
using Domain.Entities.ProductSpace;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.ProductSpace
{
    /// <summary>
    /// Группа рекомендованных товаров (ровно 4 продукта)
    /// </summary>
    public class RecommendedGroupConf : AuditableEntityConf<RecommendedGroup>
    {
        public override void Configure(EntityTypeBuilder<RecommendedGroup> builder)
        {
            base.Configure(builder);

            builder.ToTable("recommended_groups");

            // 🔹 Указываем первичный ключ
            builder.HasKey(rg => rg.Id);

            // 🔹 Поле Title (Название группы)
            builder.Property(rg => rg.Title)
                .HasColumnName("title")
                .HasMaxLength(255)
                .IsRequired();  

            // ✅ Настройка `background`
            builder.Property(rg => rg.Background)
                .HasMaxLength(500) // 🔹 Ограничиваем длину строки (URL может быть длинным)
                .IsUnicode(false)  // 🔹 Если только латиница и цифры (экономим место)
                .HasColumnName("background"); // 🔹 Определяем имя колонки
             
            builder.Property(p => p.Color)
                .HasMaxLength(20) // 🔹 Ограничиваем длину (например, "#FFFFFF" или "Red")
                .IsUnicode(false) // 🔹 Экономим место, если храним только латиницу и цифры
                .HasColumnName("color") // 🔹 Явное имя колонки
                .HasDefaultValue("#FFFFFF"); // 🔹 Значение по умолчанию (белый цвет)

        }
    }
}
