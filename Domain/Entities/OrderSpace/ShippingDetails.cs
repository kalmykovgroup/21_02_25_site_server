using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.AddressesSpace;
using Domain.Entities.AddressesSpace.Heirs;
using Domain.Entities.StatusesSpace.Heirs;

namespace Domain.Entities.OrderSpace
{
    /// <summary>
    /// Сущность, представляющая информацию о доставке.
    /// Хранит данные о получателе, адресе доставки, статусе,
    /// а также временные метки, такие как дата отправки и доставки.
    /// </summary> 
    public class ShippingDetails : AuditableEntity<ShippingDetails>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор заказа, связанного с доставкой.
        /// </summary> 
        public Guid OrderId { get; set; }

        /// <summary>
        /// Ссылка на заказ, связанный с доставкой.
        /// </summary>
        public virtual Order Order { get; set; } = null!;

        /// <summary>
        /// Имя получателя.
        /// Указывает, кому предназначена доставка.
        /// </summary> 
        public string RecipientName { get; set; } = string.Empty;

        /// <summary>
        /// Телефон получателя.
        /// Используется для связи при доставке.
        /// </summary> 
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Уникальный идентификатор адреса доставки.
        /// </summary> 
        public Guid AddressId { get; set; }

        /// <summary>
        /// Ссылка на адрес доставки.
        /// </summary>
        public virtual Address Address { get; set; } = null!;
          

        /// <summary>
        /// Уникальный идентификатор статуса доставки.
        /// </summary> 
        public Guid ShippingStatusId { get; set; }

        /// <summary>
        /// Ссылка на статус доставки.
        /// Например: "Pending", "Shipped", "Delivered".
        /// </summary>
        public virtual ShippingStatus ShippingStatus { get; set; } = null!;

        /// <summary>
        /// Дата и время отправки.
        /// Указывает, когда товар был отправлен.
        /// </summary> 
        public DateTime? ShippedAt { get; set; }

        /// <summary>
        /// Дата и время доставки.
        /// Указывает, когда товар был доставлен.
        /// </summary> 
        public DateTime? DeliveredAt { get; set; }
 
    }

}
