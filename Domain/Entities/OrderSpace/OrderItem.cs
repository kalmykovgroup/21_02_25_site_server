 
using Domain.Entities.Common; 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.ProductSpace;

namespace Domain.Entities.OrderSpace
{ 
    public class OrderItem : AuditableEntity<OrderItem>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор заказа, к которому относится элемент.
        /// </summary> 
        public Guid OrderId { get; set; }

        /// <summary>
        /// Ссылка на заказ.
        /// </summary>
        public virtual Order Order { get; set; } = null!;

        /// <summary>
        /// Идентификатор товара.
        /// </summary> 
        public Guid ProductId { get; set; }

        /// <summary>
        /// Ссылка на товар.
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Название товара на момент оформления заказа.
        /// </summary> 
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Количество товара.
        /// </summary> 
        public int Quantity { get; set; }

        /// <summary>
        /// Цена за единицу товара на момент оформления заказа.
        /// </summary> 
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Сумма за элемент заказа.
        /// </summary>
        [NotMapped]
        public decimal TotalPrice => Quantity * UnitPrice;

      
    }


}
