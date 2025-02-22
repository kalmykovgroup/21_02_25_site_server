using Domain.Entities.Common; 
using Domain.Entities.IntermediateSpace;  

namespace Domain.Entities.UserSpace
{
    /// <summary>
    /// Роль пользователя в системе (например: Admin, Moderator)
    /// </summary> 
    public class Role : AuditableEntity<Role>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Название роли (например: "Admin", "Manager")
        /// </summary> 
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание назначения роли
        /// </summary> 
        public string? Description {  get; set; }

        /// <summary>
        /// Список пользователей с этой ролью
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        /// <summary>
        /// Список разрешений роли
        /// </summary>
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

       
    }

}