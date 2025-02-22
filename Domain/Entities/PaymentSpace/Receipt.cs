using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.OrderSpace;

namespace Domain.Entities.PaymentSpace
{
    /// <summary>
    /// Сущность, представляющая чек, выданный по заказу.
    /// Хранит информацию о сумме оплаты, валюте, методе оплаты,
    /// а также ссылку на заказ, для которого был создан чек.
    /// </summary>  
    public class Receipt : AuditableEntity<Receipt>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор заказа, связанного с чеком.
        /// </summary> 
        public Guid OrderId { get; set; }

        /// <summary>
        /// Ссылка на заказ, для которого был выдан чек.
        /// </summary>
        public virtual Order Order { get; set; } = null!;

        /// <summary>
        /// Дата и время создания чека.
        /// Используется для фиксирования момента оплаты.
        /// </summary> 
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Сумма, оплаченная за заказ.
        /// Хранится с точностью до двух знаков после запятой.
        /// </summary> 
        public decimal AmountPaid { get; set; }

        /// <summary>
        /// Валюта оплаты (например, "USD", "EUR").
        /// Определяет, в какой валюте была произведена оплата.
        /// </summary> 
        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; } = null!;

        /// <summary>
        /// Способ оплаты (например, "CreditCard", "PayPal").
        /// Указывает, какой метод был использован для оплаты.
        /// </summary>  
        public Guid PaymentTypeId { get; set; }

        public virtual PaymentType PaymentType { get; set; } = null!;

        /// <summary>
        /// Уникальный идентификатор транзакции в платёжной системе.
        /// Используется для отслеживания платежей.
        /// </summary> 
        public Guid TransactionId { get; set; }

        public virtual PaymentTransaction Transaction { get; set; } = null!;

        /// <summary>
        /// URL на PDF или электронный чек.
        /// Предоставляет ссылку на сохранённую версию чека.
        /// </summary> 
        public string? ReceiptUrl { get; set; }

        /// <summary>
        /// Примечания, связанные с чеком.
        /// Например, описание скидок, налогов или других деталей оплаты.
        /// </summary> 
        public string? Notes { get; set; }

    }

}
