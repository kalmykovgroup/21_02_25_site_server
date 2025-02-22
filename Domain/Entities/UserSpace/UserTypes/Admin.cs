using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserSpace.UserTypes
{
    public class Admin : AuditableEntity<Admin>
    {
        public Guid Id { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
