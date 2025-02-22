using Domain.Entities.Common;
using Domain.Entities.UserSpace.UserTypes;
using Domain.Models.LoyaltyProgramSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.LoyaltyProgramSpace.CouponSpace
{
    /// <summary>
    /// Логирует использование купонов пользователями.
    /// </summary>
    public class CouponUsage : AuditableEntity<CouponUsage>
    {
        /// <summary>
        /// Уникальный идентификатор записи.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на купон, который использовал пользователь.
        /// </summary>
        public Guid CouponId { get; set; }

        /// <summary>
        /// Ссылка на пользователя, который применил купон.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Дата и время использования купона.
        /// </summary>
        public DateTime UsageDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Флаг, успешно ли применился купон.
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Сообщение с причиной ошибки (если купон не сработал).
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Навигационное свойство: сам купон.
        /// </summary>
        public virtual Coupon Coupon { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство: пользователь, который применил купон.
        /// </summary>
        public virtual Customer Customer { get; set; } = null!;
    }
}
