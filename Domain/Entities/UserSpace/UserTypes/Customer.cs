
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.AddressesSpace;
using Domain.Entities.OrderSpace;
using Domain.Entities.ProductSpace;
using Domain.Entities.AnalyticsSpace;
using Domain.Models.LoyaltyProgramSpace.Discount;
using Domain.Models.LoyaltyProgramSpace.Loyalty;
using Domain.Entities._Notifications;
using Domain.Entities.LoyaltyProgramSpace.CouponSpace;
using Domain.Entities.Common;
using Domain.Entities.PaymentSpace;

namespace Domain.Entities.UserSpace.UserTypes
{
    /// <summary>
    /// Клиент интернет-магазина (наследник пользователя)
    /// </summary> 
    public class Customer : AuditableEntity<Customer>
    { 
        public Guid Id { get; set; }

         
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// Идентификатор группы клиента (например, VIP, Regular)
        /// </summary> 
        public Guid CustomerGroupId { get; set; }

        /// <summary>
        /// Группа клиента
        /// </summary>
        public virtual CustomerGroup CustomerGroup { get; set; } = null!;

        /// <summary>
        /// Идентификатор предпочтений клиента
        /// </summary> 
        public Guid? CustomerPreferenceId { get; set; }

        /// <summary>
        /// Предпочтения клиента
        /// </summary>
        public virtual CustomerPreference? CustomerPreference { get; set; }

        
        /// <summary>
        /// Список методов оплаты клиента
        /// </summary>
        public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

        /// <summary>
        /// Список адресов клиента
        /// </summary>
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

        /// <summary>
        /// Список заказов клиента
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        /// <summary>
        /// Список продуктов, которые клиент ожидает
        /// </summary>
        public virtual ICollection<ProductWait> ProductWaitCollection { get; set; } = new List<ProductWait>();


        /// <summary>
        /// История просмотров клиента
        /// </summary>
        public virtual ICollection<ViewHistory> ViewHistory { get; set; } = new List<ViewHistory>();

        /// <summary>
        /// Навигационное свойство: скидки, применённые пользователем.
        /// </summary>
        public virtual ICollection<DiscountUsage> DiscountUsages { get; set; } = new List<DiscountUsage>();

        /// <summary>
        /// Навигационное свойство: записи о лояльности (CustomerLoyalty) для этого пользователя.
        /// </summary>
        public virtual ICollection<CustomerLoyalty> CustomerLoyaltyRecords { get; set; } = new List<CustomerLoyalty>();

        /// <summary>
        /// Уведомления клиента
        /// </summary>
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        /// <summary>
        /// Список купонов, использованных этим пользователем.
        /// </summary>
        public virtual ICollection<CouponUsage> CouponUsages { get; set; } = new List<CouponUsage>();



    }
}