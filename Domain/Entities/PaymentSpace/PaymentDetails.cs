using Domain.Entities.Common;
using Domain.Entities.OrderSpace;

namespace Domain.Entities.PaymentSpace
{

    public enum PaymentStatus
    {
        Pending,  // В обработке
        Authorized, // Средства заблокированы
        Paid, // Оплачено
        Failed, // Ошибка
        Refunded // Возвращено
    }

    /// <summary>
    /// Сущность, представляющая детали платежа.
    /// Хранит информацию о способе оплаты, статусе платежа,
    /// дате оплаты и связанных заказах.
    /// </summary> 
    public class PaymentDetails : AuditableEntity<PaymentDetails>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор заказа, связанного с платежом.
        /// </summary> 
        public Guid OrderId { get; set; }

        /// <summary>
        /// Ссылка на заказ, для которого осуществляется платёж.
        /// </summary>
        public virtual Order Order { get; set; } = null!;

        /// <summary>
        /// Уникальный идентификатор метода оплаты.
        /// Например, может быть идентификатором кредитной карты или другого метода.
        /// </summary> 
        public Guid PaymentMethodId { get; set; }
 
        public virtual PaymentMethod PaymentMethod { get; set; } = null!;

       

        public Guid CurrencyId { get; set; }  // Новое поле – Валюта платежа
        public virtual Currency Currency { get; set; } = null!;

        public decimal ExchangeRate { get; set; }  // Курс валюты на момент платежа

        /// <summary>
        /// Уникальный идентификатор статуса платежа.
        /// Например, "Pending", "Paid", "Failed".
        /// </summary> 
        public Guid PaymentStatusId { get; set; }

        public virtual PaymentStatus? PaymentStatus { get; set; }

        /// <summary>
        /// Если оплата произведена с использованием сохранённой карты, здесь хранится ссылка на неё.
        /// </summary>
        public Guid? PaymentCardId { get; set; }
         
        public virtual PaymentCard PaymentCard { get; set; } = null!;

        // История транзакций (первичный платеж, возвраты и т.д.).
        public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();


        public decimal TotalPaid { get; set; } // Итоговая оплата после всех списаний
        public decimal TotalRefunded { get; set; } // Общая сумма возвратов

        public bool IsAuthorized { get; set; } // Был ли платеж авторизован, но не проведен
        public decimal AuthorizedAmount { get; set; } // Сумма, заблокированная на карте

        public DateTime? PaidAt { get; set; }

       

    }

}
