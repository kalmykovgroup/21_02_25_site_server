using Domain.Entities.UserSpace; 
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.IntermediateSpace
{
    /// <summary>
    /// Связь между ролями и разрешениями (многие-ко-многим).
    /// </summary> 
    public class RolePermission
    {
        public Guid RoleId { get; set; }

        /// <summary>
        /// Навигационное свойство для роли.
        /// </summary>
        public virtual Role Role { get; set; } = null!;

        public Guid PermissionId { get; set; }

        /// <summary>
        /// Навигационное свойство для разрешения.
        /// </summary>
        public virtual Permission Permission { get; set; } = null!;


    }

}
