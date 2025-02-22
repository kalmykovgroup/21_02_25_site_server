using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.ProductSpace;
using Domain.Entities.UserSpace.UserTypes;

namespace Domain.Entities.AnalyticsSpace
{
    /// <summary>
    /// История просмотров товаров клиентами
    /// </summary> 
    public class ViewHistory : AuditableEntity<ViewHistory>
    {
        /// <summary>
        /// Уникальный идентификатор записи истории
        /// </summary> 
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор клиента, просмотревшего товар
        /// </summary> 
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Навигационное свойство клиента
        /// </summary>
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Идентификатор просмотренного товара
        /// </summary> 
        public Guid ProductId { get; set; }

        /// <summary>
        /// Навигационное свойство товара
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Дата и время просмотра товара (UTC)
        /// </summary> 
        public DateTime ViewedAt { get; set; } = DateTime.UtcNow;
         
    }

}
