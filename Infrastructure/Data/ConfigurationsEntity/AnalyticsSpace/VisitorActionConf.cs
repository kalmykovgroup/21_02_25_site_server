using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.AnalyticsSpace
{
    /// <summary>
    /// Действие посетителя в рамках сессии (например: просмотр товара, добавление в корзину и т.д.)
    /// </summary> 
    public class VisitorActionConf : AuditableEntityConf<VisitorAction>
    {

        /// <summary>
        /// Настройка сущности VisitorAction
        /// </summary>
        public override void Configure(EntityTypeBuilder<VisitorAction> entity)
        {
            base.Configure(entity);
 
            entity.ToTable("visitor_actions");

            // Первичный ключ
            entity.HasKey(v => v.Id).HasName("PK_visitor_actions");

            // Идентификатор действия
            entity.Property(v => v.Id)
                    .HasColumnName("id")
                    .IsRequired();

                
            entity.Property(v => v.VisitorSessionId).HasColumnName("visitor_session_id");

            // Связь с VisitorSession
            entity.HasOne(v => v.VisitorSession)
                    .WithMany(vs => vs.Actions)
                    .HasForeignKey(v => v.VisitorSessionId)
                    .OnDelete(DeleteBehavior.Cascade);

            // Тип действия (ограничение длины до 255 символов)
            entity.Property(v => v.ActionType)
                    .HasColumnName("action_type")
                    .HasColumnType("varchar(255)")
                    .IsRequired();

            // Время действия (UTC, timestamp)
            entity.Property(v => v.ActionTime)
                    .HasColumnName("action_time")
                    .HasColumnType("timestamp")
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Дополнительные данные (текстовое поле, например, JSON)
            entity.Property(v => v.Data)
                    .HasColumnName("data")
                    .HasColumnType("text")
                    .IsRequired(false);
 
        }
    }

}
