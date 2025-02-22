 
using System.ComponentModel.DataAnnotations.Schema; 
using Domain.Entities.Common; 
using Domain.Entities.CategorySpace;
using Domain.Entities.InventorySpace;
using Domain.Entities.IntermediateSpace;
using Domain.Entities._Storage;
using Domain.Entities.AnalyticsSpace; 
using Domain.Entities.BrandSpace;
using Domain.Models.LoyaltyProgramSpace.Bundle;
using Domain.Entities.SupplierSpace;
using Domain.Entities.OrderSpace;

namespace Domain.Entities.ProductSpace
{
    /// <summary>
    /// Сущность, представляющая продукт в интернет-магазине.
    /// Хранит данные о названии, цене, категории, поставщике, бренде,
    /// а также связанную информацию, такую как запасы, характеристики и отзывы.
    /// </summary> 
    /// 
    public class Product : SeoEntity<Product>
    {

        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор категории, к которой относится продукт.
        /// </summary> 
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Ссылка на категорию.
        /// </summary>
        public virtual Category Category { get; set; } = null!;

        /// <summary>
        /// Идентификатор поставщика, предоставившего продукт.
        /// </summary> 
        public Guid SupplierId { get; set; }

        /// <summary>
        /// Ссылка на поставщика.
        /// </summary>
       public virtual Supplier Supplier { get; set; } = null!;

        /// <summary>
        /// Цена продукта со скидкой
        /// Вычисляется, если была создана скидка.
        /// </summary>  
        public decimal Price { get; set; }

        /// <summary>
        /// Рейтинг
        /// Когда пользователь оставит отзыв о товаре, то у товара пересчетается рейтинг 
        /// </summary>  
        public decimal Rating { get; set; }

        public int NumberOfReviews { get; set; }


        /// <summary> 
        /// Процент скидки, вычисляется, если была создана скидка.
        /// </summary> 
        public decimal? DiscountPercentage { get; set; }

        /// <summary>
        /// Цена без скидок
        /// </summary> 
        public decimal? OriginalPrice { get; set; }
         

        /// <summary>
        /// Признак активности продукта (отображается ли он на сайте).
        /// </summary> 
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Уникальный идентификатор бренда.
        /// </summary> 
        public Guid BrandId { get; set; }

        /// <summary>
        /// Ссылка на бренд.
        /// </summary>
        public virtual Brand Brand { get; set; } = null!;


        /// <summary>
        /// Ссылка на картинку
        /// </summary>  
        public string Url {  get; set; } = string.Empty;

        /// <summary>
        /// Название продукта.
        /// </summary>  
        public string Name {  get; set; } = string.Empty;

        /// <summary>
        /// Описание продукта.
        /// </summary> 
        public string? Description { get; set; } = null;


        /// <summary>
        /// Список характеристик продукта.
        /// </summary>
        public virtual ICollection<ProductAttribute> Attributes { get; set; } = new List<ProductAttribute>();


        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

        /// <summary>
        /// Доступное количество продукта на складе.
        /// </summary>
        public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();



        /// <summary>
        /// Сущность, представляющая отзыв о продукте.
        /// Хранит информацию о рейтинге, комментарии, клиентах,
        /// а также файлы, связанные с отзывом.
        /// </summary>
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        /// <summary>
        /// Связанные теги продукта.
        /// </summary>
        public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

        /// <summary>
        /// Список файлов, связанных с продуктом.
        /// </summary>
        public virtual ICollection<ProductFile> ProductFiles { get; set; } = new List<ProductFile>();

        public virtual ICollection<WishListProduct> WishListProducts { get; set; } = new List<WishListProduct>();


      
        public virtual ICollection<ViewHistory> ViewHistories { get; set; } = new List<ViewHistory>();

        /// <summary>
        /// Навигационное свойство: коллекция позиций заказа (OrderItem), в которых фигурирует этот товар.
        /// </summary>
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        /// <summary>
        /// Элемент набора (bundle) товаров для групповой скидки.
        /// Связывает конкретный товар (Product) с бандл-скидкой (DiscountBundle).
        /// </summary>
        public virtual ICollection<BundleItem> BundleItems { get; set; } = new List<BundleItem>();



    }

}
