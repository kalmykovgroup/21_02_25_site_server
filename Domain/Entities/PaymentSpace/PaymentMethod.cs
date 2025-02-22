using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.UserSpace.UserTypes;

namespace Domain.Entities.PaymentSpace
{
    /// <summary>
    /// Сущность, представляющая метод оплаты.
    /// Хранит информацию о типе оплаты, связанных клиентах,
    /// а также дополнительных данных, таких как номер карты или её срок действия.
    /// </summary> 
    public class PaymentMethod : AuditableEntity<PaymentMethod>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор клиента, связанного с методом оплаты.
        /// </summary> 
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Ссылка на клиента, которому принадлежит метод оплаты.
        /// </summary>
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Тип метода оплаты (например, "CreditCard", "PayPal").
        /// Определяет, какой вид оплаты используется.
        /// </summary>  
        public Guid PaymentTypeId { get; set; }

        public virtual PaymentType PaymentType { get; set; } = null!;
         
        public Guid? PaymentCardId { get; set; }

        [ForeignKey(nameof(PaymentCardId))]
        public virtual PaymentCard PaymentCard { get; set; } = null!;

        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; } = null!;

        /// <summary>
        /// Флаг, указывающий, требуется ли ввод дополнительных данных (например, CVV).
        /// </summary>
        public bool RequiresAdditionalDetails { get; set; }

        /// <summary>
        /// Признак основного метода оплаты.
        /// Если значение true, данный метод считается основным для клиента.
        /// </summary> 
        public bool IsPrimary { get; set; } = false;

        public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    }

}
