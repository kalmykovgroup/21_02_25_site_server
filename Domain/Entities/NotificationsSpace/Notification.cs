using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations; 
using Domain.Entities.Common;
using Domain.Entities.UserSpace.UserTypes;

namespace Domain.Entities._Notifications
{

    public enum NotificationType
    {
        System
    }

    /// <summary>
    /// Уведомление, отправляемое пользователю.
    /// </summary>
    /// <remarks>
    /// Примеры использования:
    /// - Изменение статуса заказа
    /// - Персональные промо-предложения
    /// - Системные оповещения
    /// </remarks> 
    public class Notification : AuditableEntity<Notification>
    {
         
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор клиента-получателя.
        /// </summary> 
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Текст уведомления.
        /// </summary> 
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Статус прочтения.
        /// </summary> 
        public bool IsRead { get; set; } = false;

        /// <summary>
        /// Дата и время отправки.
        /// </summary> 
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Тип уведомления, пока только System.
        /// </summary>
        public NotificationType NotificationType { get; set; } = NotificationType.System;

        /// <summary>
        /// Ссылка на клиента.
        /// </summary>
        public virtual Customer Customer { get; set; } = null!;
 
       
      
    }


}
