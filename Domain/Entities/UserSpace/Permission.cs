 
using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema; 
using Domain.Entities.IntermediateSpace;  

namespace Domain.Entities.UserSpace
{
    /// <summary>
    /// Право доступа в системе (например: "CanEditProducts", "CanViewReports")
    /// </summary> 
    public class Permission : AuditableEntity<Permission>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Локализованное название разрешения
        /// </summary> 
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Локализованное описание разрешения
        /// </summary> 
        public string Description {  get; set; } = string.Empty;

        /// <summary>
        /// Список ролей, связанных с этим разрешением
        /// </summary>
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        /// <summary>
        /// Список пользователей, связанных с этим разрешением
        /// </summary>
        public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();

       
    }

}