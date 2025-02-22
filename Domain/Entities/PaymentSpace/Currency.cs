using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PaymentSpace
{
    /// <summary>
    /// Валюта оплаты (например, "USD", "EUR")
    /// </summary>
    public class Currency : AuditableEntity<Currency>
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty; //Код валюты
        public string Name { get; set; } = string.Empty; //Название валюты
    }
}
