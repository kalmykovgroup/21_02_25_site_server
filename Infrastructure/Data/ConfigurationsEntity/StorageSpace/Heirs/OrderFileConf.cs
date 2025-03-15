using Domain.Entities.StorageSpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.StorageSpace.Heirs;

namespace Infrastructure.Data.ConfigurationsEntity.StorageSpace.Heirs
{
    /// <summary>
    /// Настройка сущности OrderFile.
    /// Определяет связи с заказами.
    /// </summary>
    public class OrderFileConf : IEntityTypeConfiguration<OrderFile>
    {
       
        public void Configure(EntityTypeBuilder<OrderFile> builder)
        {
            // Указываем, что OrderFile наследует FileStorage
            builder.HasBaseType<FileStorage>();

            builder.Property(t => t.OrderId).HasColumnName("order_id");

            // Связь с заказом
            builder.HasOne(e => e.Order)
                .WithMany(o => o.OrderFiles)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
