using Domain.Entities.CategorySpace;
using Domain.Entities.Common;
using Domain.Entities.ProductSpace;
using Domain.Entities.UserSpace;
using System;
using Domain.Entities.PaymentSpace;

namespace Domain.Models.LoyaltyProgramSpace.Discount
{
    /// <summary>
    /// Оператор сравнения для сумм и других величин.
    /// </summary>
    public enum ComparisonOperator
    {
        /// <summary>
        /// Больше (>)
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Больше или равно (>=)
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// Равно (==)
        /// </summary>
        Equal,

        /// <summary>
        /// Меньше (<)
        /// </summary>
        LessThan,

        /// <summary>
        /// Меньше или равно (<=)
        /// </summary>
        LessThanOrEqual
    } 

    /// <summary>
    /// Логический оператор (связь с другими условиями в рамках одного DiscountRule).
    /// </summary>
    public enum ConditionOperator
    {
        And, // 0
        Or   // 1
    }

    /// <summary>
    /// Определяет тип условия, при котором применяется скидка.
    /// </summary>
    public enum ConditionType
    {
        /// <summary>
        /// Скидка действует, если в корзине есть товары из указанной категории.
        /// </summary>
        Category,

        /// <summary>
        /// Скидка применяется, если сумма заказа достигает определённого порога.
        /// </summary>
        CartTotal,

        /// <summary>
        /// Скидка активируется при покупке определённого количества конкретного товара.
        /// </summary>
        ProductQuantity,

        /// <summary>
        /// Скидка предоставляется пользователям, которые входят в определённый сегмент (например, совершили N заказов).
        /// </summary>
        UserSegment,

        // Новые условия

        /// <summary>
        /// Скидка активна в заданный промежуток времени суток (например, с 18:00 до 23:00).
        /// </summary>
        TimeOfDay,

        /// <summary>
        /// Скидка применяется в определённые дни недели (например, по пятницам).
        /// </summary>
        DayOfWeek,

        /// <summary>
        /// Скидка доступна только для первого заказа пользователя.
        /// </summary>
        FirstOrder,

        /// <summary>
        /// Скидка активируется, если пользователь применил определённый купон.
        /// </summary>
        CouponApplied,

        /// <summary>
        /// Скидка применяется при оплате определённым способом (например, банковской картой).
        /// </summary>
        PaymentMethod,

        /// <summary>
        /// Скидка доступна только для пользователей, входящих в указанную группу (например, VIP-клиенты).
        /// </summary>
        CustomerGroup
    }



    /// <summary>
    /// Отражает таблицу discount_conditions в БД. 
    /// Хранит настройки по конкретному условию (тип, оператор, параметры).
    /// </summary> 
    public class DiscountCondition : AuditableEntity<DiscountCondition>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на DiscountRule (FK = discount_rules.id).
        /// </summary> 
        public Guid DiscountRuleId { get; set; }

        public virtual DiscountRule DiscountRule { get; set; } = null!;

        /// <summary>
        /// Логический оператор (AND/OR) для совмещения с другими условиями в рамках одного DiscountRule.
        /// </summary> 
        public ConditionOperator Operator { get; set; }

        /// <summary>
        /// Тип условия (Category, CartTotal, ProductQuantity, UserSegment, ...)
        /// </summary> 
        public ConditionType ConditionType { get; set; }

        /// <summary>
        /// Ссылка на категорию (если условие касается товаров в категории).
        /// </summary> 
        public Guid? CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        /// <summary>
        /// Минимальное количество товаров в категории (или при ProductQuantity).
        /// </summary> 
        public int? MinQuantity { get; set; }

        /// <summary>
        /// Минимальная сумма по товарам определённой категории.
        /// </summary> 
        public decimal? MinAmount { get; set; }

        /// <summary>
        /// Ссылка на конкретный товар (если условие о количестве конкретного товара).
        /// </summary> 
        public Guid? ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Минимальное количество конкретного товара.
        /// </summary> 
        public int? Quantity { get; set; }

        /// <summary>
        /// Пороговая сумма корзины (CartTotal).
        /// </summary> 
        public decimal? Threshold { get; set; } = null!;

        /// <summary>
        /// Оператор сравнения для суммы (например, ">", ">=").
        /// </summary> 
        public ComparisonOperator? Comparison { get; set; } = null!;

        /// <summary>
        /// Для сегмента пользователей: минимальное кол-во заказов у юзера.
        /// </summary> 
        public int? MinUserOrders { get; set; } = null!;

        /// <summary>
        /// Время суток, с которого действует скидка (если ConditionType = TimeOfDay).
        /// </summary>
        public TimeSpan? StartTime { get; set; } = null!;

        /// <summary>
        /// Время суток, до которого действует скидка (если ConditionType = TimeOfDay).
        /// </summary>
        public TimeSpan? EndTime { get; set; } = null!;

        /// <summary>
        /// День недели, на который распространяется скидка (если ConditionType = DayOfWeek).
        /// 0 = Воскресенье, 1 = Понедельник, ..., 6 = Суббота.
        /// </summary>
        public int? ApplicableDayOfWeek { get; set; } = null!;

        /// <summary>
        /// Флаг: скидка применяется только для первого заказа пользователя.
        /// </summary>
        public bool? IsFirstOrder { get; set; }

        /// <summary>
        /// Ссылка на купон, который должен быть использован, чтобы скидка активировалась.
        /// </summary>
        public Guid? RequiredCouponId { get; set; }

        /// <summary>
        /// Метод оплаты, при котором действует скидка (например, "CreditCard").
        /// </summary>
        public Guid PaymentMethodId { get; set; }

        public virtual PaymentMethod? PaymentMethod { get; set; } = null!;

        /// <summary>
        /// Ссылка на группу пользователей, для которой действует скидка.
        /// </summary>
        public Guid? CustomerGroupId { get; set; }

        /// <summary>
        /// Навигационное свойство: группа клиентов.
        /// </summary>
        public virtual CustomerGroup? CustomerGroup { get; set; } = null!;

    }



}
