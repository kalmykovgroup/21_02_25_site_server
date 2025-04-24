using Application.Handlers.ProductSpace.ProductEntity.DTOs;
using Application.Handlers.ProductSpace.ProductEntity.Queries;
using Application.Handlers.ProductSpace.ProductEntity.Responses;
using AutoMapper;
using Domain.Entities.ProductSpace;
using Domain.Interfaces.Repositories.ProductSpace;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.ProductEntity.Handlers
{
    public class GetProductMainPagedHandler : IRequestHandler<GetProductsMainPagedQuery, ProductsMainPagedResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IRecommendedGroupRepository _recommendedGroupRepository;
        private readonly IMapper _mapper;

        public GetProductMainPagedHandler(IProductRepository productRepository, IRecommendedGroupRepository recommendedGroupRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _recommendedGroupRepository = recommendedGroupRepository;
            _mapper = mapper;
        }

        private const int _firstPageSize = 16; // Количество товаров на первой странице
        private const int _nextPageSize = 16;  // Количество товаров на остальных страницах*

        private const int _firstPageSizeRecommendedGroups = 2; // Количество товаров на первой странице
        private const int _nextPageSizeRecommendedGroups = 2;  // Количество товаров на остальных страницах*

        public async Task<ProductsMainPagedResponse> Handle(GetProductsMainPagedQuery request, CancellationToken cancellationToken)
        {
            var (products, hasMoreProducts) = await _productRepository.GetAllSellerOffersAsync(
                null, 
                null,
                request.Page,
                _firstPageSize,
                _nextPageSize,
                cancellationToken );

            (IEnumerable<RecommendedGroup> recommendedGroups, bool hasMoreRecommendedGroups) = await _recommendedGroupRepository.GetGroups(request.Page, _firstPageSizeRecommendedGroups, _nextPageSizeRecommendedGroups);

            var productsDto = _mapper.Map<IEnumerable<ShortSellerOfferDto>>(products);

            var recommendedGroupsDto = _mapper.Map<IEnumerable<RecommendedGroupDto>>(recommendedGroups); 


            return new ProductsMainPagedResponse(productsDto, hasMoreProducts, recommendedGroupsDto, hasMoreRecommendedGroups, request.Page);
        }
    }
}
