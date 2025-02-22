using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserSpace
{
    public class PhoneVerificationCode
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int CountSendMessage { get; set; } //Кол-во отправленных одного и того же кода
        public int NumberOfAttempts { get; set; } //Кол-во попыток отправить
        public int AllCountSendMessage { get; set; } //Кол-во всего отправленных на этот номер
        public long CodeLifetimeSeconds { get; set; } 
        public long UnblockingTimeSeconds { get; set; }

        public int NumberOfMessagesSentBeforeLoggingIn { get; set; }
    }
}
