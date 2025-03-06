using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductSpace.ProductEntity.Responses
{
    public class GetProductNameSuggestionsResponse : BaseResponse
    {
        public List<string> Suggestions { get; set; }

        public GetProductNameSuggestionsResponse(List<string> suggestions)
        {
            Suggestions = suggestions;
        }
    }
}
