using Domain.Entities.AnalyticsSpace;
using Domain.Entities.CategorySpace; 
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.IntermediateSpace
{
    /// <summary>
    /// Связь между предпочтениями клиента и категориями товаров (многие-ко-многим).
    /// </summary> 
    public class CustomerPreferenceCategory
    {
        /// <summary>
        /// Идентификатор предпочтения клиента.
        /// </summary> 
        public Guid CustomerPreferenceId { get; set; }

        /// <summary>
        /// Навигационное свойство предпочтения клиента.
        /// </summary>
        public virtual CustomerPreference CustomerPreference { get; set; } = null!;

        /// <summary>
        /// Идентификатор категории товаров.
        /// </summary> 
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Навигационное свойство категории товаров.
        /// </summary>
        public virtual Category Category { get; set; } = null!;

      
    }

}
