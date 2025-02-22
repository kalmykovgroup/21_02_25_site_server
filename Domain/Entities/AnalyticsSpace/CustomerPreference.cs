using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.UserSpace.UserTypes;

namespace Domain.Entities.AnalyticsSpace
{
    /// <summary>
    /// Предпочтения клиента для персонализации взаимодействия (Настройки)
    /// </summary> 
    public class CustomerPreference : AuditableEntity<CustomerPreference>
    {
         
        public Guid Id { get; set; }


        /// <summary>
        /// Связь с любимыми категориями через посредник
        /// </summary>
        public virtual ICollection<CustomerPreferenceCategory> FavoriteCategoryLinks { get; set; } = new List<CustomerPreferenceCategory>();

        /// <summary>
        /// Связь с любимыми брендами через посредник
        /// </summary>
        public virtual ICollection<CustomerPreferenceBrand> FavoriteBrandLinks { get; set; } = new List<CustomerPreferenceBrand>();

        /// <summary>
        /// Частота уведомлений (например: Daily, Weekly)
        /// </summary> 
        public string NotificationFrequency { get; set; } = "Daily";

        /// <summary>
        /// Флаг получения уведомлений о скидках
        /// </summary> 
        public bool ReceiveDiscountNotifications { get; set; } = true;

        /// <summary>
        /// Флаг получения уведомлений о новинках
        /// </summary> 
        public bool ReceiveNewArrivalNotifications { get; set; } = true;

      
        
    }

}
