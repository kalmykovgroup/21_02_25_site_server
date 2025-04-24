using Application.Handlers.ProductSpace.ProductEntity.DTOs;
using Application.Handlers.ProductSpace.ProductEntity.Queries;
using Application.Handlers.ProductSpace.ProductEntity.Responses;
using AutoMapper; 
using Domain.Interfaces.Repositories.ProductSpace;
using MediatR; 

namespace Application.Handlers.ProductSpace.ProductEntity.Handlers
{
    public class GetFilteredProductsHandler : IRequestHandler<GetFilteredProductsQuery, ProductsPagedResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        
         private const int _firstPageSize = 30; // Количество товаров на первой странице
         private const int _nextPageSize = 20;  // Количество товаров на остальных страницах*

        public GetFilteredProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
         

        public async Task<ProductsPagedResponse> Handle(GetFilteredProductsQuery request, CancellationToken cancellationToken)
        {
            var (products, hasMore) = await _productRepository.GetAllSellerOffersAsync(
                                                                         request.Search,
                                                                         request.CategoryId,
                                                                         request.Page,
                                                                         _firstPageSize,
                                                                         _nextPageSize,
                                                                         cancellationToken
                                                                     );

            var productsDto = _mapper.Map<IEnumerable<ShortSellerOfferDto>>(products);

            return new ProductsPagedResponse(productsDto, hasMore, request.Page);

        }
    }

}
