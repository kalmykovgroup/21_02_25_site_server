 
using Domain.Entities.Common; 
using System.ComponentModel.DataAnnotations.Schema; 
using Domain.Entities.IntermediateSpace; 
using Domain.Entities.ProductSpace;
using Domain.Entities.AddressesSpace.Heirs;

namespace Domain.Entities.SupplierSpace
{
    /// <summary>
    /// Поставщик товара.
    /// Хранит информацию о поставщике, включая контактные данные, основной адрес и связанные бренды.
    /// </summary> 
    public class Supplier : AuditableEntity<Supplier>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Название поставщика.
        /// </summary> 
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание поставщика.
        /// </summary> 
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Контактный телефон поставщика.
        /// </summary> 
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Электронная почта поставщика.
        /// </summary> 
        public string? Email { get; set; }
  
      
        /// <summary>
        /// Признак активности поставщика (например, для скрытия старых или неактивных поставщиков).
        /// </summary> 
        public bool IsActive { get; set; } = true;
         
        /// <summary>
        /// Список адресов (например, склады или офисы).
        /// </summary> 
        public virtual ICollection<SupplierAddress> Addresses { get; set; } = new List<SupplierAddress>();


        /// <summary>
        /// Список брендов, связанных с поставщиком.
        /// </summary>
        [InverseProperty(nameof(SupplierBrand.Supplier))]
        public virtual ICollection<SupplierBrand> SupplierBrands { get; set; } = new List<SupplierBrand>();

        /// <summary>
        /// Список продуктов, предоставляемых поставщиком.
        /// </summary>
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

  
    }


}
