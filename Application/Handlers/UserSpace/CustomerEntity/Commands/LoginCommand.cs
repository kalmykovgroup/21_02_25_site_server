using Application.Handlers.UserSpace.CustomerEntity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserSpace.CustomerEntity.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
