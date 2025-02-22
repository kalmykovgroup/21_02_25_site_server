using Domain.Entities.Common; 
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.ProductSpace;

namespace Domain.Entities.AnalyticsSpace
{
    /// <summary>
    /// Элемент отчёта о продажах
    /// </summary> 
    public class SalesReportItem : AuditableEntity<SalesReportItem>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор отчёта о продажах
        /// </summary> 
        public Guid SalesReportId { get; set; }

        /// <summary>
        /// Навигационное свойство отчёта о продажах
        /// </summary>
        public virtual SalesReport SalesReport { get; set; } = null!;

        /// <summary>
        /// Идентификатор товара
        /// </summary> 
        public Guid ProductId { get; set; }

        /// <summary>
        /// Навигационное свойство товара
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Количество проданных единиц
        /// </summary> 
        public int QuantitySold { get; set; }

        /// <summary>
        /// Выручка от продажи товара
        /// </summary> 
        public decimal Revenue { get; set; }
         
    }

}
