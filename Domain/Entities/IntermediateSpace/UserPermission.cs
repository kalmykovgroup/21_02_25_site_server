using Domain.Entities.UserSpace; 
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.IntermediateSpace
{
    /// <summary>
    /// Связь между пользователями и разрешениями (многие-ко-многим).
    /// </summary> 
    public class UserPermission
    { 
        public Guid UserId { get; set; }

        /// <summary>
        /// Навигационное свойство для пользователя.
        /// </summary>
        public virtual User User { get; set; } = null!;
         
        public Guid PermissionId { get; set; }

        /// <summary>
        /// Навигационное свойство для разрешения.
        /// </summary>
        public virtual Permission Permission { get; set; } = null!;
 
    }

}
