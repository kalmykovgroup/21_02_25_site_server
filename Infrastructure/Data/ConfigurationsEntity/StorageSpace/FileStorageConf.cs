using Domain.Entities.StorageSpace;
using Domain.Entities.StatusesSpace;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.StorageSpace.Heirs;

namespace Infrastructure.Data.ConfigurationsEntity.StorageSpace
{
    /// <summary>
    /// Настройка сущности FileStorage.
    /// Определяет дискриминатор TPH и дополнительные ограничения.
    /// </summary>
    public class FileStorageConf : AuditableEntityConf<FileStorage>
    {
        public override void Configure(EntityTypeBuilder<FileStorage> entity)
        {
            base.Configure(entity);

            // Настройка TPH для дискриминатора FileCategory
            entity.HasDiscriminator<FileCategory>(t => t.FileCategory)
                    .HasValue<FileStorage>(FileCategory.GeneralFile) 
                    .HasValue<OrderFile>(FileCategory.OrderFile)
                    .HasValue<ReviewFile>(FileCategory.ReviewFile);

            entity.ToTable("files_storage");
            entity.HasKey(x => x.Id);
            entity.Property(e => e.Id).HasColumnName("id");
                
            entity.Property(e => e.FileCategory).HasColumnName("file_category");
            entity.Property(e => e.FileName).IsRequired().HasColumnName("file_name").HasMaxLength(255);
            entity.Property(e => e.FilePath).IsRequired().HasColumnName("file_path").HasMaxLength(2048);
            entity.Property(e => e.FileType).IsRequired().HasColumnName("file_type").HasMaxLength(100);
            entity.Property(e => e.FileSize).IsRequired().HasColumnName("file_size");
            entity.Property(e => e.IsPrimary).IsRequired().HasColumnName("is_primary"); 
            
        }
    }
}
