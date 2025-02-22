
using Domain.Entities.Common;
using Domain.Models.LoyaltyProgramSpace.Bundle; 

namespace Domain.Models.LoyaltyProgramSpace.Discount
{
    /// <summary>
    /// Тип скидки, применяемой в рамках скидочного правила.
    /// </summary>
    public enum DiscountRuleType
    {
        /// <summary>
        /// Процентная скидка, например, 10% от стоимости товара.
        /// </summary>
        Percentage,

        /// <summary>
        /// Фиксированная скидка в определённой валюте, например, 500 рублей.
        /// </summary>
        FixedAmount,

        /// <summary>
        /// Скидка, применяемая только к определённой категории товаров.
        /// </summary>
        CategorySpecific,

        /// <summary>
        /// Скидка на набор товаров (бандл), например, "Купи 2, получи скидку".
        /// </summary>
        Bundle,

        /// <summary>
        /// Использование бонусных баллов лояльности для получения скидки.
        /// </summary>
        LoyaltyPoints,

        /// <summary>
        /// Бесплатная доставка при определённых условиях.
        /// </summary>
        FreeShipping,

        /// <summary>
        /// Акция "Купи X товаров, получи Y бесплатно".
        /// </summary>
        BuyXGetY
    }

    /// <summary>
    /// Основное правило скидки. Определяет параметры скидки (тип, значение, период действия и т.д.).
    /// </summary>
    public class DiscountRule : AuditableEntity<DiscountRule>
    {
        /// <summary>
        /// Уникальный идентификатор скидочного правила.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название скидки, например "Летняя распродажа 2024".
        /// </summary> 
        public string? Name { get; set; } = null;

        /// <summary>
        /// Описание скидки (подробности для админ-панели, комментарии).
        /// </summary> 
        public string? Description { get; set; } = null;

        /// <summary>
        /// Тип скидки (процентная, фиксированная, бандл и т.п.).
        /// </summary>
        public DiscountRuleType DiscountRuleType { get; set; }

        /// <summary>
        /// Значение скидки (процент или фиксированная сумма).
        /// </summary>
        public decimal? Value { get; set; }

        /// <summary>
        /// Дата начала действия скидки (UTC). Может быть null, если не ограничено.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания действия скидки (UTC). Может быть null, если не ограничено.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Минимальная сумма заказа, при которой скидка начинает действовать.
        /// </summary>
        public decimal? MinOrderAmount { get; set; }

        /// <summary>
        /// Максимальное количество использований скидки (0 или null = без ограничений).
        /// </summary>
        public int? MaxUsageCount { get; set; }

        /// <summary>
        /// Текущее количество использований скидки (для отслеживания лимита).
        /// </summary>
        public int? CurrentUsageCount { get; set; }

        /// <summary>
        /// Признак возможности суммироваться с другими скидками.
        /// </summary>
        public bool? IsStackable { get; set; }

        /// <summary>
        /// Приоритет применения скидки. Чем выше значение, тем раньше проверяется скидка.
        /// </summary>
        public int? Priority { get; set; }

        /// <summary>
        /// Флаг: является ли скидка эксклюзивной (нельзя сочетать с другими скидками).
        /// </summary>
        public bool IsExclusive { get; set; }


        /// <summary>
        /// Связанные условия скидки (логические правила, категории, товары и т.д.).
        /// </summary>
        public virtual ICollection<DiscountCondition> DiscountConditions { get; set; }
            = new List<DiscountCondition>();

        /// <summary>
        /// Связанные групповые скидки (наборы товаров).
        /// </summary>
        public virtual ICollection<DiscountBundle> DiscountBundles { get; set; }
            = new List<DiscountBundle>();

        /// <summary>
        /// Коллекция купонов, привязанных к данному правилу (если нужно).
        /// </summary>
        public virtual ICollection<Coupon> Coupons { get; set; }
            = new List<Coupon>();

        /// <summary>
        /// История использования скидки (позволяет отслеживать, кто и когда применил).
        /// </summary>
        public virtual ICollection<DiscountUsage> DiscountUsages { get; set; }
            = new List<DiscountUsage>();
    }


}
