using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Common;
using Domain.Entities.IntermediateSpace;
using Domain.Entities._Storage;
using Domain.Models.LoyaltyProgramSpace.Discount;
using Domain.Entities.UserSpace.UserTypes;
using Domain.Entities.StatusesSpace.Heirs;
using Domain.Entities.PaymentSpace;

namespace Domain.Entities.OrderSpace
{ 
    public class Order : AuditableEntity<Order>
    {
         
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный номер заказа для отображения клиенту.
        /// </summary>  
        public string OrderNumber { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор клиента, оформившего заказ.
        /// </summary> 
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Общая сумма заказа до скидок и налогов.
        /// </summary> 
        public decimal SubTotal { get; set; }

        /// <summary>
        /// Общая сумма скидок, применённых к заказу.
        /// </summary> 
        public decimal TotalDiscount { get; set; }

        /// <summary>
        /// Сумма налога на заказ.
        /// </summary> 
        public decimal TaxAmount { get; set; }

        /// <summary>
        /// Общая сумма заказа после скидок и налогов.
        /// </summary> 
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Идентификатор статуса заказа.
        /// </summary> 
        public Guid OrderStatusId { get; set; }
        public virtual OrderStatus OrderStatus { get; set; } = null!;

        /// <summary>
        /// Навигационное свойство: скидки, применённые пользователем.
        /// </summary>
        public virtual ICollection<DiscountUsage> DiscountUsages { get; set; } = new List<DiscountUsage>();


         /// <summary>
          /// Идентификатор информации о платеже..
          /// </summary> 
          public Guid PaymentDetailsId { get; set; }
          public virtual PaymentDetails PaymentDetails { get; set; } = null!;


          /// <summary>
          /// Информация о доставке..
          /// </summary> 
          public Guid ShippingDetailsId { get; set; }
          public virtual ShippingDetails ShippingDetails { get; set; } = null!;

        /// <summary>
        /// Примечания к заказу.
        /// </summary> 
        public string? Notes { get; set; }

         /// <summary>
        /// Список товаров в заказе.
        /// </summary>
        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        /// <summary>
        /// Список купонов, применённых к заказу.
        /// </summary>
          public virtual ICollection<OrderCoupon> OrderCoupons { get; set; } = new List<OrderCoupon>();

          

        /// <summary>
        /// История изменений статуса заказа.
        /// </summary>
        public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

        

        /// <summary>
        /// Список файлов, связанных с заказом.
        /// </summary>
        public virtual ICollection<OrderFile> OrderFiles { get; set; } = new List<OrderFile>();

        
      
    }
}
