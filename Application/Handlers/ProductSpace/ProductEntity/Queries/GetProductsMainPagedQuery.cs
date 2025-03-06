using Application.Handlers.ProductSpace.ProductEntity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.ProductEntity.Queries
{
    public record GetProductsMainPagedQuery(int Page) : IRequest<ProductsMainPagedResponse>; 
}
