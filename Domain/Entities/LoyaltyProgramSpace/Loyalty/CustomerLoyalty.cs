using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.LoyaltyProgramSpace.Loyalty
{
    using Domain.Entities.Common;
    using Domain.Entities.UserSpace.UserTypes;
    using System;

    /// <summary>
    /// Отражает участие пользователя в программе лояльности и хранит текущие баллы и уровень.
    /// </summary>
    public class CustomerLoyalty : AuditableEntity<CustomerLoyalty>
    {
        /// <summary>
        /// Уникальный идентификатор записи.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на пользователя, который участвует в программе лояльности.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Ссылка на программу лояльности, к которой относится эта запись.
        /// </summary>
        public Guid LoyaltyProgramId { get; set; }

        /// <summary>
        /// Текущий уровень лояльности пользователя (например, Bronze, Silver, Gold).
        /// Может быть null, если пользователь ещё не достиг минимального уровня.
        /// </summary>
        public Guid? CurrentTierId { get; set; }

        /// <summary>
        /// Текущее (или общее) количество баллов, накопленных пользователем в данной программе.
        /// </summary>
        public int TotalPoints { get; set; }

        /// <summary>
        /// Признак, участвует ли пользователь активно в программе (true) или приостановлен (false).
        /// </summary>
        public bool Active { get; set; }
 

        /// <summary>
        /// Навигационное свойство: ссылка на пользователя (Customer).
        /// </summary>
        public virtual Customer? Customer { get; set; }

        /// <summary>
        /// Навигационное свойство: ссылка на программу лояльности (LoyaltyProgram).
        /// </summary>
        public virtual LoyaltyProgram? LoyaltyProgram { get; set; }

        /// <summary>
        /// Навигационное свойство: текущий уровень лояльности (LoyaltyTier).
        /// </summary>
        public virtual LoyaltyTier? CurrentTier { get; set; }
    }


}
