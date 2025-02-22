
using Domain.Entities.BrandSpace;
using Domain.Entities.SupplierSpace;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.IntermediateSpace
{
    /// <summary>
    /// Связь между поставщиками и брендами (многие-ко-многим)
    /// </summary> 
    public class SupplierBrand
    {
        /// <summary>
        /// Идентификатор поставщика
        /// </summary> 
        public Guid SupplierId { get; set; }

        /// <summary>
        /// Навигационное свойство поставщика
        /// </summary>
        public virtual Supplier Supplier { get; set; } = null!;

        /// <summary>
        /// Идентификатор бренда
        /// </summary> 
        public Guid BrandId { get; set; }

        /// <summary>
        /// Навигационное свойство бренда
        /// </summary>
        public virtual Brand Brand { get; set; } = null!;

     
    }
}
