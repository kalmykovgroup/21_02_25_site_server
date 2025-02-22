using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.UserSpace;
using Domain.Entities.Common;

namespace Domain.Entities.PaymentSpace
{
    /// <summary>
    /// Хранение сведений о платёжной карте пользователя (только данные для повторного использования).
    /// Реальные данные карты не сохраняются, а используются токен и маскированный номер.
    /// </summary>
    public class PaymentCard : AuditableEntity<PaymentCard>
    { 
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на пользователя, которому принадлежит карта.
        /// </summary>
        public Guid UserId { get; set; } 

        public virtual User User { get; set; } = null!;

        /// <summary>
        /// Тип карты (например, Visa, MasterCard).
        /// </summary> 
        public string CardType { get; set; } = string.Empty;

        /// <summary>
        /// Последние 4 цифры карты.
        /// </summary> 
        public string Last4Digits { get; set; } = string.Empty;

        /// <summary>
        /// Дата истечения срока действия.
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// Токен, полученный от платёжного провайдера для проведения транзакций.
        /// </summary> 
        public string Token { get; set; } = string.Empty; //Использовать алгоритмы шифрования (AES) 

        /// <summary>
        /// Маскированный номер карты (например, "**** **** **** 1234").
        /// </summary> 
        public string MaskedCardNumber { get; set; } = string.Empty;
    }
}
