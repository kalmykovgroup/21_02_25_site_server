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
    /// Настройка сущности Review.
    /// Определяет связи с продуктами, клиентами и файлами.
    /// </summary>
    public class ReviewConf : AuditableEntityConf<Review>
    {
        public override void Configure(EntityTypeBuilder<Review> entity)
        {
            base.Configure(entity);

            // Связь с продуктом
            entity.HasOne(e => e.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Связь с клиентом
                entity.HasOne(e => e.Customer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
          
        }
    }
}
