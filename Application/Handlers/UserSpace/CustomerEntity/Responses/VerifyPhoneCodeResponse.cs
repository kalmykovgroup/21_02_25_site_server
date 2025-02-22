using Application.Common;
using Application.Handlers.UserSpace.CustomerEntity.DTOs;
using Domain.Entities.UserSpace.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserSpace.CustomerEntity.Responses
{
    public class VerifyPhoneCodeResponse : BaseResponse
    {
        public string MessageInformation { get; set; } = string.Empty;
        public string? Token { get; set; }
        public CustomerDto? CustomerDto { get; set; }
    }
}
