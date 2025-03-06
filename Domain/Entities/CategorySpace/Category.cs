 
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Common;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.ProductSpace; 

namespace Domain.Entities.CategorySpace
{
    /// <summary>
    /// Категория товаров в системе
    /// </summary> 
    public class Category : SeoEntity<Category>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор родительской категории (для иерархии)
        /// </summary> 
        public Guid? ParentCategoryId { get; set; }


        public virtual Category? ParentCategory { get; set; } = null!;

        /// <summary>
        /// Признак активности категории
        /// </summary> 
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// URL изображения категории
        /// </summary> 
        public string? Icon { get; set; }

 
        /// <summary>
        /// Название продукта.
        /// </summary>  
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание продукта.
        /// </summary> 
        public string? Description {  get; set; } = String.Empty;
 

        /// <summary>
        /// Уровень вложенности категории
        /// </summary> 
        public int Level { get; set; }

        /// <summary>
        /// При выводе, нужно понимать кто будет в каком порядке
        /// </summary> 
        public int Index { get; set; }

        /// <summary>
        /// Полный путь категории в иерархии
        /// </summary> 
        public string? FullPath { get; set; }
         

        public string? Path { get; set; }
         

        /// <summary>
        /// Предпочтения пользователей, связанные с категорией
        /// </summary>
        public virtual ICollection<CustomerPreferenceCategory> CustomerPreferenceCategories { get; set; } = new List<CustomerPreferenceCategory>();

        /// <summary>
        /// Дочерние подкатегории
        /// </summary>
        public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();

        /// <summary>
        /// Товары в этой категории
        /// </summary>
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

      
    }

}
