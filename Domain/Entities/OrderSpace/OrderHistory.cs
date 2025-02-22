using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.OrderSpace;
using Domain.Entities.StatusesSpace.Heirs;

namespace Domain.Entities.OrderSpace
{ 
    public class OrderHistory : AuditableEntity<OrderHistory>
    {
        /// <summary>
        /// Уникальный идентификатор записи истории заказа.
        /// </summary> 
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор заказа, к которому относится запись.
        /// </summary> 
        public Guid OrderId { get; set; }

        /// <summary>
        /// Ссылка на заказ, для которого ведётся история изменений.
        /// </summary>
        public virtual Order Order { get; set; } = null!;

        /// <summary>
        /// Уникальный идентификатор статуса из справочника OrderStatus.
        /// </summary> 
        public Guid OrderStatusId { get; set; }

        /// <summary>
        /// Ссылка на статус заказа.
        /// </summary>
        public virtual OrderStatus OrderStatus { get; set; } = null!;

        /// <summary>
        /// Дополнительные комментарии или причины изменения статуса.
        /// Например: "Клиент отменил заказ".
        /// </summary> 
        public string? Comments { get; set; }

       
    }

}
