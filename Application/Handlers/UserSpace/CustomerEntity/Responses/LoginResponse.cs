using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserSpace.CustomerEntity.Responses
{
    public class LoginResponse : BaseResponse {  
        /// <summary>
        /// Время через которое нам дадут доступ
        /// </summary>
        public long UnblockingTime { get; set; }

        public string PhoneNumber { get; set; }

        public string? MessageInformation { get; set; } = null;

        public LoginResponse(long unblockingTime, string phoneNumber)
        {
            UnblockingTime = unblockingTime;
            PhoneNumber = phoneNumber;
        }
    }
}
