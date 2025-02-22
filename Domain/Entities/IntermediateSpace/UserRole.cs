 
using Domain.Entities.UserSpace; 
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.IntermediateSpace
{
    /// <summary>
    /// Связь между пользователями и ролями (многие-ко-многим)
    /// </summary> 
    public class UserRole
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary> 
        public Guid UserId { get; set; }

        /// <summary>
        /// Навигационное свойство пользователя
        /// </summary>
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// Идентификатор роли
        /// </summary> 
        public Guid RoleId { get; set; }

        /// <summary>
        /// Навигационное свойство роли
        /// </summary>
        public virtual Role Role { get; set; } = null!;

     
    }
}
