using Application.Handlers.ProductSpace.ProductEntity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.ProductEntity.Queries
{
    public class GetProductNameSuggestionsQuery: IRequest<GetProductNameSuggestionsResponse>
    {
        public string Query { get; set; }

        public GetProductNameSuggestionsQuery(string query)
        {
            Query = query;
        }
    }
}
