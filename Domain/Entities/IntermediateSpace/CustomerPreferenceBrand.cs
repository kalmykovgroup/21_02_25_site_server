using Domain.Entities.AnalyticsSpace;
using Domain.Entities.BrandSpace;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.IntermediateSpace
{
    /// <summary>
    /// Связь между предпочтениями клиента и брендами (многие-ко-многим).
    /// </summary> 
    public class CustomerPreferenceBrand
    {
        /// <summary>
        /// Идентификатор предпочтения клиента.
        /// </summary> 
        public Guid CustomerPreferenceId { get; set; }

        /// <summary>
        /// Навигационное свойство к предпочтению клиента.
        /// </summary>
        public virtual CustomerPreference CustomerPreference { get; set; } = null!;

        /// <summary>
        /// Идентификатор бренда.
        /// </summary> 
        public Guid BrandId { get; set; }

        /// <summary>
        /// Навигационное свойство к бренду.
        /// </summary>
        public virtual Brand Brand { get; set; } = null!;

        
    }

}
