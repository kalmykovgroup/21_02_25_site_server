 
using System.ComponentModel.DataAnnotations.Schema; 
using Domain.Entities.Common; 
using Domain.Entities.CategorySpace;
using Domain.Entities.InventorySpace;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.StorageSpace;
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
        
        public int NumberOfReviews { get; set; }
 
        
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
        /// Название продукта.
        /// </summary>  
        public string Name {  get; set; } = string.Empty;

        /// <summary>
        /// Описание продукта.
        /// </summary> 
        public string? Description { get; set; } = null;

        
        /// <summary>
        /// Доступное количество продукта на складе.
        /// </summary>
        public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();
        
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

        
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

        public virtual ICollection<RecommendedGroupProduct> RecommendedGroupProducts { get; set; } = new List<RecommendedGroupProduct>();

    
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
