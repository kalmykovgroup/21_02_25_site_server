
using Application.Handlers.ProductSpace.ProductEntity.DTOs;
using Application.Handlers.ProductSpace.ProductEntity.Queries;
using AutoMapper;
using Domain.Entities.ProductSpace;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.ProductSpace;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.ProductEntity.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, LongProductDto>
    {
        public GetProductByIdHandler(
            IProductRepository productRepository,
            IMapper mapper,
            ILogger<GetProductByIdHandler> logger) { }

        public Task<LongProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
