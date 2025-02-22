using Domain.Entities.UserSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
