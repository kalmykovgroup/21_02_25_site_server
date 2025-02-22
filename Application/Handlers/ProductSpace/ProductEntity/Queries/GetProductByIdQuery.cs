using Application.Common.Interfaces;
using Application.Handlers.ProductSpace.ProductEntity.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.ProductEntity.Queries
{
    public class GetProductByIdQuery : IRequest<LongProductDto>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
