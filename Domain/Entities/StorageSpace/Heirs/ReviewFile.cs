
using Domain.Entities.ProductSpace;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities._Storage
{
    /// <summary>
    /// Сущность, представляющая файл, связанный с отзывом.
    /// Используется для хранения изображений или других файлов, добавленных к отзывам клиентов.
    /// </summary>
    public class ReviewFile : FileStorage
    {

        public ReviewFile() {
            FileCategory = FileCategory.ReviewFile;
        }

        /// <summary>
        /// Уникальный идентификатор отзыва, к которому относится файл.
        /// </summary> 
        public Guid ReviewId { get; set; }

        /// <summary>
        /// Ссылка на отзыв, с которым связан файл.
        /// </summary>
        public virtual Review Review { get; set; } = null!;

       
    }

}
