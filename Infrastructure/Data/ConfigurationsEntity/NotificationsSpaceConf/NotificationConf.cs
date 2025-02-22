using Domain.Entities._Notifications; 
using Infrastructure.Data.ConfigurationsEntity.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace Infrastructure.Data.ConfigurationsEntity.NotificationsSpaceConf
{
    /// <summary>
    /// Настройка сущности Notification.
    /// Определяет связи с клиентами и переводами.
    /// </summary>
    public class NotificationConf : AuditableEntityConf<Notification>
    {
        public override void Configure(EntityTypeBuilder<Notification> builder)
        {
            base.Configure(builder);

            builder.ToTable("notification");

            builder.Property(e => e.CustomerId).HasColumnName("customer_id");
            builder.Property(e => e.Message).HasColumnName("message").IsRequired().HasMaxLength(255);
            builder.Property(e => e.IsRead).HasColumnName("is_read");
            builder.Property(e => e.SentAt).HasColumnName("sent_at");
            builder.Property(e => e.NotificationType).HasColumnName("notification_type");

            // Связь с клиентом
            builder.HasOne(n => n.Customer)
                .WithMany(c => c.Notifications)
                .HasForeignKey(n => n.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
