 
using Domain.Entities.OrderSpace;
using Domain.Models.LoyaltyProgramSpace;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.IntermediateSpace
{
    /// <summary>
    /// Связующая сущность между заказом и применённым купоном
    /// </summary>
    /// <remarks>
    /// Фиксирует факт применения конкретного купона в заказе.
    /// Позволяет отслеживать историю использования купонов и предотвращать повторное применение.
    /// </remarks> 
    public class OrderCoupon
    { 
        public Guid OrderId { get; set; }

        /// <summary>
        /// Ссылка на заказ
        /// </summary>
        public virtual Order Order { get; set; } = null!;
         
        public Guid CouponId { get; set; }

        /// <summary>
        /// Ссылка на купон
        /// </summary>
        public virtual Coupon Coupon { get; set; } = null!;
         
        public decimal DiscountAmount { get; set; }

       
    }

}
