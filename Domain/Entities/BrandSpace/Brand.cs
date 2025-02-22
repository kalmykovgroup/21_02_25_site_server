using Domain.Entities.IntermediateSpace; 
using System.ComponentModel.DataAnnotations.Schema; 
using Domain.Entities.Common;
using Domain.Entities.ProductSpace; 

namespace Domain.Entities.BrandSpace
{
    /// <summary>
    /// Бренд товаров в системе
    /// </summary> 
    public class Brand : SeoEntity<Brand>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Локализованное название бренда
        /// </summary> 
        public string Name  { get; set; } = string.Empty;

        /// <summary>
        /// Локализованное описание бренда
        /// </summary> 
        public string? Description {  get; set; }

        /// <summary>
        /// URL-адрес логотипа бренда
        /// </summary> 
        public string? LogoUrl { get; set; }

        /// <summary>
        /// Признак активности бренда в системе
        /// </summary> 
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Товары бренда
        /// </summary>
        [InverseProperty(nameof(Product.Brand))]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Связь с предпочтениями пользователей
        /// </summary>
        [InverseProperty(nameof(CustomerPreferenceBrand.Brand))]
        public virtual ICollection<CustomerPreferenceBrand> CustomerPreferenceBrands { get; set; } = new List<CustomerPreferenceBrand>();

        /// <summary>
        /// Связь с поставщиками бренда
        /// </summary>
        [InverseProperty(nameof(SupplierBrand.Brand))]
        public virtual ICollection<SupplierBrand> SupplierBrands { get; set; } = new List<SupplierBrand>();

    
 
    }
}
