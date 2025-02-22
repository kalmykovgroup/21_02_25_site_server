using Domain.Entities.Common;
using Domain.Entities.ProductSpace; 

namespace Domain.Entities.InventorySpace
{
    public class SupplyProduct : AuditableEntity<SupplyProduct>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на поставку
        /// </summary>
        public Guid SupplyId { get; set; }
        public virtual Supply Supply { get; set; } = null!;

        /// <summary>
        /// Ссылка на продукт
        /// </summary>
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Количество поставленного товара
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Закупочная цена
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Дата поступления на склад
        /// </summary>
        public DateTime ReceivedDate { get; set; }
    }
}
