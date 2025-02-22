using Application.Handlers.ProductSpace.WishListEntity.DTOs;
using Application.Handlers.ProductSpace.WishListEntity.Queries;
using Application.Handlers.ProductSpace.WishListEntity.Responses;
using AutoMapper;
using Domain.Entities.ProductSpace;
using Domain.Interfaces.Repositories.IntermediateSpace;
using Domain.Interfaces.Repositories.ProductSpace;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.WishListEntity.Handlers
{
    public class GetWishListHandler : IRequestHandler<GetWishListQuery, GetWishListResponse>
    {
        private readonly IWishListRepository _iWishListRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetWishListHandler> _logger;

        public GetWishListHandler(
            IWishListRepository iWishListRepository,
             IMapper mapper,
            ILogger<GetWishListHandler> logger)
        {
            _iWishListRepository = iWishListRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetWishListResponse> Handle(GetWishListQuery request, CancellationToken cancellationToken)
        {
            List<Product> products = await _iWishListRepository.GetWishListProductsAsync(request.WishListId, cancellationToken);

            List<WishListItemDto> items = _mapper.Map<List<WishListItemDto>>(products);

            return new GetWishListResponse()
            {
                WishList = items,
                Success = true
            };

        }
    }
}
