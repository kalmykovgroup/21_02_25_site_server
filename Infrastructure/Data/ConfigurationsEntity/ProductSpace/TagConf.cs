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
    /// Настройка сущности Tag.
    /// Определяет связи и дополнительные ограничения.
    /// </summary>
    public class TagConf : AuditableEntityConf<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> entity)
        {
            base.Configure(entity);

            // Связь с ProductTag
            entity.HasMany(e => e.ProductTags)
                    .WithOne(pt => pt.Tag)
                    .HasForeignKey(pt => pt.TagId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Индекс для быстрого поиска по имени тега
                entity.HasIndex(e => e.Name)
                    .IsUnique()
                    .HasDatabaseName("IX_Tag_Name");
           
        }
    }
}
