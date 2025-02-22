using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema; 
using Domain.Entities.UserSpace.UserTypes;

namespace Domain.Entities.UserSpace
{
    /// <summary>
    /// Группа клиентов (например: VIP, Новый клиент, Премиум)
    /// </summary> 
    public class CustomerGroup : AuditableEntity<CustomerGroup>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Название группы клиентов
        /// </summary>  
        public string Name {  get; set; } = string.Empty;

        /// <summary>
        /// Описание группы клиентов (необязательное)
        /// </summary> 
        public string? Description { get; set; } = null;

        /// <summary>
        /// Список клиентов, относящихся к группе
        /// </summary>
        public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    }
}
