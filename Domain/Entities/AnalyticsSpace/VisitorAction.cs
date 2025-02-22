using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations; 

namespace Domain.Entities.AnalyticsSpace
{
    /// <summary>
    /// Действие посетителя в рамках сессии (например: просмотр товара, добавление в корзину и т.д.)
    /// </summary> 
    public class VisitorAction : AuditableEntity<VisitorAction>
    {
         
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор сессии посетителя
        /// </summary> 
        public Guid VisitorSessionId { get; set; }
 
        public virtual VisitorSession VisitorSession { get; set; } = null!;

        /// <summary>
        /// Тип действия (например: просмотр товара)
        /// </summary> 
        public string ActionType { get; set; } = string.Empty;

        /// <summary>
        /// Время совершения действия (UTC)
        /// </summary> 
        public DateTime ActionTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дополнительные данные действия (например: JSON, параметры)
        /// </summary> 
        public string? Data { get; set; }
         
    }

}
