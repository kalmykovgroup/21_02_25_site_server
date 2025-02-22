using Domain.Entities.ProductSpace;
using Domain.Models.LoyaltyProgramSpace.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.IntermediateSpace
{
    /// <summary>
    /// Сущность для связи товаров со скидками.
    /// Позволяет одному товару участвовать в нескольких скидках.
    /// </summary>
    public class ProductDiscount
    {

        /// <summary>
        /// Ссылка на товар, к которому применяется скидка.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Ссылка на правило скидки, которое применяется к товару.
        /// </summary>
        public Guid DiscountRuleId { get; set; }

        /// <summary>
        /// Навигационное свойство: товар.
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство: скидочное правило.
        /// </summary>
        public virtual DiscountRule DiscountRule { get; set; } = null!;
    }
}
