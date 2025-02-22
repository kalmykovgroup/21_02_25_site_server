using Application.Handlers.CategorySpace.CategoryEntity.DTOs;
using Application.Handlers.CategorySpace.CategoryEntity.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.CategorySpace.CategoryEntity.Queries
{
    public record GetCategoriesQuery : IRequest<GetCategoryResponse>;
    
}
