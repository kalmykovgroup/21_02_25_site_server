using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.AnalyticsSpace
{
    /// <summary>
    /// Отчёт о продажах за указанный период
    /// </summary> 
    public class SalesReportConf : AuditableEntityConf<SalesReport>
    {

        /// <summary>
        /// Настройка сущности SalesReport
        /// </summary>
        public override void Configure(EntityTypeBuilder<SalesReport> entity)
        {
              base.Configure(entity); 

                entity.ToTable("sales_reports");

                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                 
                entity.Property(x => x.PeriodStart)
                    .HasColumnName("period_start")
                    .HasColumnType("timestamp")
                    .IsRequired(); 

                entity.Property(x => x.PeriodEnd)
                    .HasColumnName("period_end")
                    .HasColumnType("timestamp")
                    .IsRequired();

                // ✅ Общая выручка (decimal(18,2))
                entity.Property(x => x.TotalRevenue)
                    .HasColumnName("total_revenue")
                    .HasPrecision(18, 2)
                    .IsRequired();

                // ✅ Количество заказов за период
                entity.Property(x => x.TotalOrders)
                    .HasColumnName("total_orders")
                    .IsRequired();

                // ✅ Количество проданных товаров
                entity.Property(x => x.TotalProductsSold)
                    .HasColumnName("total_products_sold")
                    .IsRequired();
                 
            
        }
    }

}
