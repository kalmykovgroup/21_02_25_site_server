using Domain.Entities.Common;
using Domain.Models.LoyaltyProgramSpace.Loyalty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.LoyaltyProgramSpace.Loyalty
{
    /// <summary>
    /// Временный бонус, который увеличивает начисление баллов в рамках программы лояльности.
    /// </summary>
    public class LoyaltyBonus : AuditableEntity<LoyaltyBonus>
    {
        /// <summary>
        /// Уникальный идентификатор бонуса.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на программу лояльности, в которой действует этот бонус.
        /// </summary>
        public Guid LoyaltyProgramId { get; set; }

        /// <summary>
        /// Дата начала действия бонуса.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания действия бонуса.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Множитель баллов, который применяется во время действия бонуса.
        /// Например, 2.0 означает удвоенное начисление баллов.
        /// </summary>
        public decimal Multiplier { get; set; }

        /// <summary>
        /// Навигационное свойство: Программа лояльности.
        /// </summary>
        public virtual LoyaltyProgram LoyaltyProgram { get; set; } = null!;
    }
}
