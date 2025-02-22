using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities._Storage;
using Domain.Entities.UserSpace.UserTypes;

namespace Domain.Entities.ProductSpace
{
    /// <summary>
    /// Сущность, представляющая отзыв о продукте.
    /// Хранит информацию о рейтинге, комментарии, клиентах,
    /// а также файлы, связанные с отзывом.
    /// </summary> 
    public class Review : AuditableEntity<Review>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Уникальный идентификатор продукта, на который оставлен отзыв.
        /// </summary> 
        public Guid ProductId { get; set; }

        /// <summary>
        /// Ссылка на продукт, к которому относится отзыв.
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Уникальный идентификатор клиента, оставившего отзыв.
        /// </summary> 
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Ссылка на клиента, оставившего отзыв.
        /// </summary>
        public virtual Customer Customer { get; set; } = null!;

        /// <summary>
        /// Рейтинг отзыва (от 1 до 5).
        /// </summary> 
        public int? Rating { get; set; }

        /// <summary>
        /// Текст комментария.
        /// Может содержать мнение клиента о продукте.
        /// </summary> 
        public string? Comment { get; set; }

        /// <summary>
        /// Файлы, прикреплённые к отзыву.
        /// Например, фотографии продукта.
        /// </summary>
        public virtual ICollection<ReviewFile> ReviewFiles { get; set; } = new List<ReviewFile>();

        
       
    }

}
