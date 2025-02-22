using Domain.Entities.UserSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AddressesSpace.Heirs
{
    public class UserAddress : Address
    {
        public UserAddress()
        {
            AddressType = AddressType.User;
        }

        public Guid UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
