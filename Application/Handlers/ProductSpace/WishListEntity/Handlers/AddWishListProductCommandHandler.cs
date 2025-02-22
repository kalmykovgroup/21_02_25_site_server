using Application.Common.Interfaces;
using Application.Handlers.ProductSpace.WishListEntity.Commands;
using Application.Handlers.ProductSpace.WishListEntity.Responses;
using Application.Handlers.UserSpace.CustomerEntity.Commands;
using Application.Handlers.UserSpace.CustomerEntity.Responses;
using Domain.Entities.IntermediateSpace;
using Domain.Entities.OrderSpace;
using Domain.Entities.ProductSpace;
using Domain.Entities.UserSpace;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.IntermediateSpace;
using Domain.Interfaces.Repositories.UserSpace;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.WishListEntity.Handlers
{
    public class AddWishListProductCommandHandler : IRequestHandler<AddWishListProductCommand, AddWishListProductResponse>
    {
        private readonly IWishListProductRepository _iWishListProductRepository;

        private readonly ILogger<AddWishListProductCommandHandler> _logger;

        public AddWishListProductCommandHandler(
            IWishListProductRepository iWishListProductRepository,
            ILogger<AddWishListProductCommandHandler> logger)
        {
            _iWishListProductRepository = iWishListProductRepository;
            _logger = logger;
        }

        public async Task<AddWishListProductResponse> Handle(AddWishListProductCommand request, CancellationToken cancellationToken)
        {



            foreach (WishListProductPair pair in request.Batch)
            {
                 
                if (await _iWishListProductRepository.ExistsAsync(request.WishListId, pair.ProductId, cancellationToken))
                {
                    if (!pair.IsFavorite) {

                        await _iWishListProductRepository.DeleteAsync(request.WishListId, pair.ProductId, cancellationToken);
                    }

                }
                else
                {
                    if (pair.IsFavorite)
                    {
                        await _iWishListProductRepository.AddAsync(
                              new WishListProduct()
                              {
                                  ProductId = pair.ProductId,
                                  WishListId = request.WishListId,
                              },
                              cancellationToken);
                    }
                }
            }

  
            _logger.LogInformation($"\n\n Запись успешно сохранена\n\n ");

            return new AddWishListProductResponse()
            {
                Success = true,  
            };

        }
    }
}

