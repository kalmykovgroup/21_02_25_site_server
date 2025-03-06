using Application.Handlers.ProductSpace.ProductEntity.DTOs;
using Application.Handlers.ProductSpace.ProductEntity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.ProductEntity.Queries
{
    public record GetFilteredProductsQuery(string? Search, Guid? CategoryId, int Page) : IRequest<ProductPagedResponse>;

}
