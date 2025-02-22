using Domain.Entities.AddressesSpace;
using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserSpace.UserTypes
{
    /// <summary>
    /// Сотрудник системы (наследник пользователя)
    /// </summary> 
    public class Employee : AuditableEntity<Employee>
    {
         
        public Guid Id { get; set; }

         
        public virtual User User { get; set; } = null!;
         
         
        /// <summary>
        /// Должность сотрудника (например, "Менеджер")
        /// </summary> 
        public string Position { get; set; } = string.Empty;

        /// <summary>
        /// Дата приёма на работу (UTC)
        /// </summary> 
        public DateTime HiredAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата увольнения (если применимо)
        /// </summary> 
        public DateTime? TerminatedAt { get; set; }

        /// <summary>
        /// Дополнительные заметки о сотруднике
        /// </summary> 
        public string? Notes { get; set; }



    }
}