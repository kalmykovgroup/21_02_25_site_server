using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations; 

namespace Domain.Entities.AnalyticsSpace
{
    /// <summary>
    /// Отчёт о продажах за указанный период
    /// </summary> 
    public class SalesReport : AuditableEntity<SalesReport>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Дата начала периода отчёта
        /// </summary> 
        public DateTime PeriodStart { get; set; }

        /// <summary>
        /// Дата окончания периода отчёта
        /// </summary> 
        public DateTime PeriodEnd { get; set; }

        /// <summary>
        /// Общая выручка за период
        /// </summary> 
        public decimal TotalRevenue { get; set; }

        /// <summary>
        /// Общее количество заказов за период
        /// </summary> 
        public int TotalOrders { get; set; }

        /// <summary>
        /// Общее количество проданных товаров за период
        /// </summary> 
        public int TotalProductsSold { get; set; }

        /// <summary>
        /// Продукты с наибольшим количеством продаж
        /// </summary>
        public virtual ICollection<SalesReportItem> TopSellingProducts { get; set; } = new List<SalesReportItem>();
         
    }

}
