using Domain.Entities.Common;
using Domain.Entities.Common.Interfaces;
using Domain.Entities.IntermediateSpace;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ProductSpace
{
    /// <summary>
    /// Группа рекомендованных товаров  
    /// </summary>
    public class RecommendedGroup : AuditableEntity<RecommendedGroup>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Название группы (например, "Популярные товары", "Топ скидки")
        /// </summary>
        public string Title { get; set; } = string.Empty;
        public string? Background { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;

        public virtual ICollection<RecommendedGroupProduct> RecommendedGroupProducts { get; set; } = null!;
       
    }
}
