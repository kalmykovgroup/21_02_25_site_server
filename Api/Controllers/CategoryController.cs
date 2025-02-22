using Application.Handlers.CategorySpace.CategoryEntity.DTOs;
using Application.Handlers.CategorySpace.CategoryEntity.Queries;
using Application.Handlers.CategorySpace.CategoryEntity.Responses;
using Application.Handlers.ProductSpace.ProductEntity.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IMediator mediator, IMapper mapper, ILogger<CategoryController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }
        

        [HttpGet]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        { 
            GetCategoryResponse response = await _mediator.Send(new GetCategoriesQuery(), cancellationToken);

            return Content(response.Categories, "application/json"); 
        }
         
    }
}
