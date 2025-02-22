

using Domain.Entities.AddressesSpace;
using Domain.Entities.AddressesSpace.Heirs;
using Domain.Entities.Common;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.ProductSpace;
using Domain.Entities.UserSpace.UserTypes; 

namespace Domain.Entities.UserSpace
{

    public enum UserType
    {
        Customer,  // Клиент интернет-магазина
        Employee,  // Работник (продавец, менеджер)
        Admin      // Администратор системы
    }

    /// <summary>  
    /// Это базовый тип, ссылка на который есть у Employee, Customer, Admin.
    /// При этом это отдельная таблица
    /// </summary> 
    public class User : AuditableEntity<User>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Тип пользователя
        /// </summary> 
        public UserType UserType { get; set; }

        /// <summary>
        /// Номер телефона пользователя
        /// </summary> 
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Имя пользователя
        /// </summary> 
        public string? FirstName { get; set; } = null;

        /// <summary>
        /// Фамилия пользователя
        /// </summary> 
        public string? LastName { get; set; } = null;

        /// <summary>
        /// Фамилия пользователя
        /// </summary> 
        public string? Patronymic { get; set; } = null;

        public DateTime? DateOfBirth { get; set; } = null;

        /// <summary>
        /// Электронная почта пользователя
        /// </summary> 
        public string? Email { get; set; }

        /// <summary>
        /// Хэш пароля пользователя
        /// </summary> 
        public string? PasswordHash { get; set; } = string.Empty;
      

        public Guid WishListId { get; set; }
        /// <summary>
        /// Избранное
        /// </summary>
        public virtual WishList WishList { get; set; } = null!;

        /// <summary>
        /// Статус активности аккаунта
        /// </summary> 
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Тип адреса (склады, пользовательские, поставщик)  
        /// </summary>  
        public AddressType AddressType { get; set; } = AddressType.User;
        /// <summary>
        /// Список адресов 
        /// </summary> 
        public virtual ICollection<UserAddress> Addresses { get; set; } = new List<UserAddress>();

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        /// <summary>
        /// Персональные разрешения пользователя
        /// </summary>
        public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();

        public virtual Customer Customer { get; set; } = null!;

        public virtual Employee Employee { get; set; } = null!;

        public virtual Admin Admin { get; set; } = null!;

    }

}
