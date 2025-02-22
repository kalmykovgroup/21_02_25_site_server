using Domain.Entities._Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.ConfigurationsEntity.StorageSpace.Heirs
{
    /// <summary>
    /// Настройка сущности ReviewFile.
    /// Определяет связи с отзывами.
    /// </summary>
    public class ReviewFileConf : IEntityTypeConfiguration<ReviewFile>
    {
        public void Configure(EntityTypeBuilder<ReviewFile> entity)
        { 
            // Указываем, что ReviewFile наследует FileStorage
            entity.HasBaseType<FileStorage>();

            entity.Property(t => t.ReviewId).HasColumnName("review_id");

            // Связь с отзывом
            entity.HasOne(e => e.Review)
                .WithMany(r => r.ReviewFiles)
                .HasForeignKey(e => e.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);       
        }
    }
}
