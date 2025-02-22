﻿using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.PaymentSpace
{
    public class PaymentType : AuditableEntity<PaymentType>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
