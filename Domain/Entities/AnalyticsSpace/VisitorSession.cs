using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations; 

namespace Domain.Entities.AnalyticsSpace
{
    /// <summary>
    /// Сессия посетителя сайта
    /// </summary> 
    public class VisitorSession : AuditableEntity<VisitorSession>
    {
        
        public Guid Id { get; set; }

        /// <summary>
        /// IP-адрес посетителя
        /// </summary> 
        public string IPAddress { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор посетителя (из cookies)
        /// </summary> 
        public string? VisitorId { get; set; }

        /// <summary>
        /// Дата и время начала сессии (UTC)
        /// </summary> 
        public DateTime SessionStart { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата и время завершения сессии (UTC)
        /// </summary> 
        public DateTime? SessionEnd { get; set; }

        /// <summary>
        /// Источник перехода (поисковик, соцсети и т.д.)
        /// </summary> 
        public string? TrafficSource { get; set; }

        /// <summary>
        /// Список действий в рамках сессии
        /// </summary>
        public virtual ICollection<VisitorAction> Actions { get; set; } = new List<VisitorAction>();
         
    }
}
