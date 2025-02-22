using Application.Handlers.CategorySpace.CategoryEntity.DTOs;
using AutoMapper;
using Domain.Entities.CategorySpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.CategorySpace.CategoryEntity.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {

            CreateMap<Category, CategoryDto>();
        }
    }
}
