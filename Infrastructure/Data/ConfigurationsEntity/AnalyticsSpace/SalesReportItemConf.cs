using Domain.Entities.Common; 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.AnalyticsSpace
{
    /// <summary>
    /// Элемент отчёта о продажах
    /// </summary> 
    public class SalesReportItemConf : AuditableEntityConf<SalesReportItem>
    {

        /// <summary>
        /// Настройка сущности SalesReportItem
        /// </summary>
        public override void Configure(EntityTypeBuilder<SalesReportItem> entity)
        {
            base.Configure(entity);

             
            entity.HasKey(e => new { e.SalesReportId, e.ProductId });

            entity.Property(e => e.Revenue).HasPrecision(18, 2);

            entity.Property(e => e.QuantitySold)
                .IsRequired();

            entity.HasOne(e => e.SalesReport)
                .WithMany(sr => sr.TopSellingProducts)
                .HasForeignKey(e => e.SalesReportId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
          
        }
    }

}
