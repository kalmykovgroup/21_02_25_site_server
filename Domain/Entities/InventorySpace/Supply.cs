using Domain.Entities.Common;
using Domain.Entities.SupplierSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.InventorySpace
{
    public class Supply : AuditableEntity<Supply>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Дата поставки
        /// </summary>
        public DateTime SupplyDate { get; set; }

        /// <summary>
        /// Ссылка на поставщика
        /// </summary>
        public Guid SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;

        /// <summary>
        /// Номер накладной или заказа у поставщика
        /// </summary>
        public string InvoiceNumber { get; set; } = string.Empty;

        /// <summary>
        /// Статус поставки (на складе, в пути, отменена)
        /// </summary>
        public string Status { get; set; } = "Pending";

        /// <summary>
        /// Связь с продуктами в этой поставке
        /// </summary>
        public virtual ICollection<SupplyProduct> SupplyProducts { get; set; } = new List<SupplyProduct>();
    }
}
