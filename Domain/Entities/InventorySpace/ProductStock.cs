using Domain.Entities.UserSpace;
using Domain.Entities.Common; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.ProductSpace;

namespace Domain.Entities.InventorySpace
{
    /// <summary>
    /// Запас товара на конкретном складе
    /// </summary>
    /// <remarks>
    /// Пример: 
    /// - Склад "Москва-Центральный": 85 единиц товара X
    /// - Склад "СПб-Ленинградский": 120 единиц товара Y
    /// </remarks> 
    public class ProductStock : AuditableEntity<ProductStock>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор склада
        /// </summary>
        /// <remarks>
        /// - Пример: "a8b9c1d2-3e4f-5g6h-7i8j-9k0l1m2n3o4p"
        /// </remarks> 
        public Guid WarehouseId { get; set; }

        /// <summary>
        /// Ссылка на склад
        /// </summary>
        public virtual Warehouse Warehouse { get; set; } = null!;

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        /// <remarks>
        /// - Пример: "b5c6d7e8-f9g0-h1i2-j3k4-l5m6n7o8p9q0"
        /// </remarks> 
        public Guid ProductId { get; set; }

        /// <summary>
        /// Ссылка на товар
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Доступное количество
        /// </summary>
        /// <remarks>
        /// - Обновляется автоматически при продажах/поставках
        /// - Пример: 45
        /// </remarks> 
        public int StockQuantity { get; set; }

        /// <summary>
        /// Минимальный запас для оповещений
        /// </summary>
        /// <remarks>
        /// - При достижении триггерается уведомление менеджерам
        /// - Пример: 10 (заказ новых партий при остатке меньше 10)
        /// </remarks> 
        public int MinimumStockLevel { get; set; } = 0;

 
       
    }
}
