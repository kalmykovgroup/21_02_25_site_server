 
using Domain.Entities.UserSpace;
using Domain.Entities.Common; 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.AnalyticsSpace
{
    /// <summary>
    /// История просмотров товаров клиентами
    /// </summary> 
    public class ViewHistoryConf : AuditableEntityConf<ViewHistory>
    {

        /// <summary>
        /// Настройка сущности ViewHistory
        /// </summary>
        public override void Configure(EntityTypeBuilder<ViewHistory> entity)
        {
            base.Configure(entity);
            
            // Составной ключ
            entity.HasKey(vh => new { vh.CustomerId, vh.ProductId });

            // Индекс для ускорения поиска по CustomerId
            entity.HasIndex(vh => vh.CustomerId)
                .HasDatabaseName("IX_ViewHistory_CustomerId");

            // Индекс для ускорения поиска по ProductId
            entity.HasIndex(vh => vh.ProductId)
                .HasDatabaseName("IX_ViewHistory_ProductId");

            // Связь с UserCustomer
            entity.HasOne(vh => vh.Customer)
                .WithMany(c => c.ViewHistory)
                .HasForeignKey(vh => vh.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с ProductEntity
            entity.HasOne(vh => vh.Product)
                .WithMany(p => p.ViewHistories)
                .HasForeignKey(vh => vh.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Поле даты просмотра
            entity.Property(vh => vh.ViewedAt)
                .IsRequired();
            
        }
    }

}
