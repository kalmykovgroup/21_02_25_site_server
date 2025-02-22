using Application.Handlers.ProductSpace.ProductEntity.DTOs;
using Application.Handlers.ProductSpace.ProductEntity.Queries;
using Application.Handlers.ProductSpace.ProductEntity.Responses;
using AutoMapper; 
using Domain.Interfaces.Repositories.ProductSpace;
using MediatR; 

namespace Application.Handlers.ProductSpace.ProductEntity.Handlers
{
    public class GetFilteredProductsHandler : IRequestHandler<GetFilteredProductsQuery, ProductPagedResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper; 

        public GetFilteredProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
         

        public async Task<ProductPagedResult> Handle(GetFilteredProductsQuery request, CancellationToken cancellationToken)
        {
            var (products, hasMore) = await _productRepository.GetAllProductsAsync(
                                                                         request.Search,
                                                                         request.CategoryId,
                                                                         request.Page,
                                                                         cancellationToken
                                                                     );

            var productsDto = _mapper.Map<IEnumerable<ShortProductDto>>(products);

            return new ProductPagedResult(productsDto, hasMore, request.Page);

        }
    }

}
