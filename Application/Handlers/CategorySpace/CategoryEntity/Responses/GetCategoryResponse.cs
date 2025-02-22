using Application.Common;
using Application.Handlers.CategorySpace.CategoryEntity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.CategorySpace.CategoryEntity.Responses
{
    public class GetCategoryResponse : BaseResponse
    {
        
       public string Categories {  get; set; } = string.Empty;
    }
}
