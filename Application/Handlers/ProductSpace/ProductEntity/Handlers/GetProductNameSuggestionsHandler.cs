using Application.Handlers.ProductSpace.ProductEntity.Queries;
using Application.Handlers.ProductSpace.ProductEntity.Responses;
using AutoMapper;
using Domain.Interfaces.Repositories.ProductSpace;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.ProductEntity.Handlers
{
    public class GetProductNameSuggestionsHandler : IRequestHandler<GetProductNameSuggestionsQuery, GetProductNameSuggestionsResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductNameSuggestionsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetProductNameSuggestionsResponse> Handle(GetProductNameSuggestionsQuery request, CancellationToken cancellationToken)
        {
            List<string> suggestions = await _productRepository.GetProductNameSuggestionsAsync(request.Query);

            return new GetProductNameSuggestionsResponse(suggestions);
        }
    }
}
