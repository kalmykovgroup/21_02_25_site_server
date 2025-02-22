 
using Domain.Entities.OrderSpace; 
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities._Storage
{
    /// <summary>
    /// Сущность, представляющая файл, связанный с заказом.
    /// Используется для хранения документов, изображений или других файлов, связанных с заказами.
    /// </summary>
    public class OrderFile : FileStorage
    {
        public OrderFile()
        {
            FileCategory = FileCategory.OrderFile;
        }

        /// <summary>
        /// Уникальный идентификатор заказа, к которому относится файл.
        /// </summary> 
        public Guid OrderId { get; set; }

        /// <summary>
        /// Ссылка на заказ, с которым связан файл.
        /// </summary>
        public virtual Order Order { get; set; } = null!;

      
     
    }

}
