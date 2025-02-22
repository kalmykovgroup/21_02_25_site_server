

using Domain.Entities.Common;
using Domain.Entities.OrderSpace;
using Domain.Entities.UserSpace.UserTypes;

namespace Domain.Models.LoyaltyProgramSpace.Discount
{


    /// <summary>
    /// Отражает факт использования скидки конкретным пользователем в рамках определённого заказа.
    /// </summary>
    public class DiscountUsage : AuditableEntity<DiscountUsage>
    {
        /// <summary>
        /// Уникальный идентификатор записи использования скидки.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на правило скидки (если применялось конкретное DiscountRule).
        /// </summary>
        public Guid? DiscountRuleId { get; set; }

        /// <summary>
        /// Ссылка на пользователя, который воспользовался скидкой.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Ссылка на заказ, к которому была применена скидка (может быть null, если заказ не оформлен или отменён).
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Дата и время (UTC) использования скидки.
        /// </summary>
        public DateTime? UsageDate { get; set; }

        /// <summary>
        /// Итоговая сумма скидки, применённая в денежном эквиваленте.
        /// </summary>
        public decimal? AppliedAmount { get; set; }

        /// <summary>
        /// Навигационное свойство для правила скидки (DiscountRule).
        /// </summary>
        public virtual DiscountRule? DiscountRule { get; set; }

        /// <summary>
        /// Навигационное свойство для пользователя (Customer), который применил скидку.
        /// </summary>
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство для заказа (Order), где применена скидка.
        /// </summary>
        public virtual Order Order { get; set; } = null!;
    }

}
