using Domain.Entities.ProductSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.IntermediateSpace
{
    public class RecommendedGroupProduct
    {
        public Guid ProductId { get; set; }

        public Guid RecommendedGroupId { get; set; }


        public virtual Product Product { get; set; } = null!;
        public virtual RecommendedGroup RecommendedGroup { get; set; } = null!;
    }
}
