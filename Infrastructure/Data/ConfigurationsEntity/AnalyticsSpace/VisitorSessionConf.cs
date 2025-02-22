using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities.AnalyticsSpace
{
    /// <summary>
    /// Сессия посетителя сайта
    /// </summary>
    [Table("visitor_sessions")]
    public class VisitorSessionConf : AuditableEntityConf<VisitorSession>
    {

        /// <summary>
        /// Настройка сущности VisitorSession
        /// </summary>
        public override void Configure(EntityTypeBuilder<VisitorSession> entity)
        {
            base.Configure(entity);
 
            entity.ToTable("visitor_sessions");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasColumnName("id");

            // ✅ IP-адрес посетителя
            entity.Property(x => x.IPAddress)
                .HasColumnName("ip_address")
                .HasMaxLength(45)
                .IsRequired();

            // ✅ Идентификатор посетителя (из cookies)
            entity.Property(x => x.VisitorId)
                .IsRequired(false)
                .HasColumnName("visitor_id")
                .HasMaxLength(255);
                 
            entity.Property(x => x.SessionStart)
                .HasColumnName("session_start")
                .HasColumnType("timestamp")
                .IsRequired();
                 
            entity.Property(x => x.SessionEnd)
                .IsRequired(false)
                .HasColumnName("session_end")
                .HasColumnType("timestamp");

            // ✅ Источник перехода (поисковик, соцсети и т.д.)
            entity.Property(x => x.TrafficSource)
                .IsRequired(false)
                .HasColumnName("traffic_source")
                .HasMaxLength(255);
 
           
        }
    }
}
