using Domain.Entities.SupplierSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AddressesSpace.Heirs
{
    public class SupplierAddress : Address
    {
        public SupplierAddress()
        {
            AddressType = AddressType.Supplier;
        }

        public Guid SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; } = null!;
    }
}
