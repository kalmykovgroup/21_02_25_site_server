 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Common;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.LoyaltyProgramSpace.CouponSpace;
using Domain.Models.LoyaltyProgramSpace.Discount;

namespace Domain.Models.LoyaltyProgramSpace
{
    /// <summary>
    /// Купон, который предоставляет определённую скидку при выполнении условий.
    /// </summary>
    public class Coupon : AuditableEntity<Coupon>
    {
        /// <summary>
        /// Уникальный идентификатор купона.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный код купона, например "SUMMER2024".
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Дата истечения срока действия купона (UTC). Может быть null, если бессрочный.
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Максимальное количество использований купона (0 или null = без ограничений).
        /// </summary>
        public int? MaxUses { get; set; }

        /// <summary>
        /// Текущее количество использований купона (для отслеживания лимита).
        /// </summary>
        public int? CurrentUses { get; set; }

        /// <summary>
        /// Флаг: можно ли использовать купон один раз на пользователя.
        /// </summary>
        public bool? IsSingleUsePerUser { get; set; }

        /// <summary>
        /// Ссылка на правило скидки, связанное с купоном.
        /// </summary>
        public Guid? DiscountRuleId { get; set; }
         

        /// <summary>
        /// Навигационное свойство для правила скидки.
        /// </summary>
        public virtual DiscountRule? DiscountRule { get; set; }


        /// <summary>
        /// Список использований этого купона.
        /// </summary>
        public virtual ICollection<CouponUsage> CouponUsages { get; set; } = new List<CouponUsage>();


        public virtual ICollection<OrderCoupon> OrderCoupons { get; set; } = new List<OrderCoupon>();
    }

}
