using Domain.Entities.Common;  

namespace Domain.Entities.OrderSpace
{
    /// <summary>
    /// Сущность, представляющая различные способы доставки.
    /// Например, "Курьер", "Самовывоз", "Почта".
    /// Хранит информацию о названии метода, стоимости и его описании.
    /// </summary>  
    public class ShippingMethod : AuditableEntity<ShippingMethod>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Название способа доставки.
        /// Например, "Курьер" или "Самовывоз".
        /// </summary> 
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание способа доставки.
        /// Например, "Доставка курьером до двери в течение 2 дней".
        /// </summary>  
        public string? Description {  get; set; } = string.Empty;

        /// <summary>
        /// Стоимость доставки.
        /// Значение хранится с точностью до двух знаков после запятой.
        /// Например, 500 рублей.
        /// </summary> 
        public decimal Cost { get; set; }

        /// <summary>
        /// Признак активности способа доставки.
        /// Если значение false, данный способ доставки не отображается для выбора.
        /// </summary> 
        public bool IsActive { get; set; } = true;

        
    }

}
