
using Domain.Entities.ProductSpace;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities._Storage
{
    /// <summary>
    /// Сущность, представляющая файл, связанный с продуктом.
    /// Используется для хранения информации об изображениях, документах или других файлах, связанных с продуктом.
    /// </summary>
    public class ProductFile : FileStorage
    {
        public ProductFile()
        {
            FileCategory = FileCategory.ProductFile;
        }

        /// <summary>
        /// Уникальный идентификатор продукта, к которому относится файл.
        /// </summary> 
        public Guid ProductId { get; set; }

        /// <summary>
        /// Ссылка на продукт, с которым связан файл.
        /// </summary>
        public virtual Product Product { get; set; } = null!;

      
 
    }

}
