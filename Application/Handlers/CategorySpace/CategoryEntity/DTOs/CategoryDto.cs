using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.CategorySpace.CategoryEntity.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public string? Icon { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? Path { get; set; } 

        public int Level { get; set; }

        public int Index { get; set; }

        public string? FullPath { get; set; }

        public List<CategoryDto> SubCategories { get; set; } = new List<CategoryDto>();
    }
}
