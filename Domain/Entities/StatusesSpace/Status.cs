
using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema; 
namespace Domain.Entities.StatusesSpace
{
    public enum StatusType {
        Unknown,
        OrderStatus, 
        ShippingStatus
    }


    /// <summary> 
    /// Типа сущности TPH.
    /// Это подход, при котором несколько классов иерархии наследования сопоставляются с одной таблицей в базе данных
    /// Базовый класс для динамических статусов.
    /// Используется для управления статусами заказов, платежей и доставки.
    /// </summary> 
    public class Status : AuditableEntity<Status>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Порядок сортировки статусов.
        /// Позволяет отображать статусы в определённой последовательности.
        /// </summary> 
        public int SortOrder { get; set; }

        /// <summary>
        /// Признак статуса по умолчанию.
        /// Если значение true, данный статус используется как начальный.
        /// </summary> 
        public bool IsDefault { get; set; }

        public StatusType StatusType { get; set; } = StatusType.Unknown;

        /// <summary>
        /// Локализованное название статуса.
        /// Получается из переводов на основе текущей культуры.
        /// </summary> 
        public string Name { get; set; } = string.Empty; 
    }

}
