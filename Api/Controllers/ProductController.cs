  
using Application.Handlers.ProductSpace.ProductEntity.DTOs;
using Application.Handlers.ProductSpace.ProductEntity.Queries;
using Application.Handlers.ProductSpace.ProductEntity.Responses;
using Application.Handlers.ProductSpace.WishListEntity.Commands;
using Application.Handlers.ProductSpace.WishListEntity.Queries;
using Application.Handlers.ProductSpace.WishListEntity.Responses;
using AutoMapper; 
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator; 
        private readonly ILogger<ProductController> _logger;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator; 
            _logger = logger;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<LongProductDto>> GetById(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<ProductPagedResult>> GetAll(
            [FromQuery] string? search,
            [FromQuery] Guid? categoryId,
            [FromQuery] int page = 1)
        {
            if (page < 1) page = 1; // ✅ Гарантируем, что страница не отрицательная

            _logger.LogWarning($"Получение {page} страницы");

            var result = await _mediator.Send(new GetFilteredProductsQuery(search, categoryId, page));
            return Ok(result);
        }

  
        [HttpPost("add-to-wish-list")]
        [Authorize]
        public async Task<IActionResult> AddToWishList([FromBody] List<WishListProductPair> batch)
        {

            foreach (WishListProductPair pair in batch)
            {

                _logger.LogWarning($"{pair.ProductId} | {pair.IsFavorite}");
            }

                Guid? wishListId = User.GetWishListId();

            _logger.LogError("Запрос");

            if (wishListId == null)
            {
                _logger.LogError($"Не найден wish_list_id в claim {wishListId}");
                return Unauthorized();
            }
 
              AddWishListProductResponse res = await _mediator.Send(new AddWishListProductCommand(batch, (Guid)wishListId));
            
              
            return Ok(res);
        }

        [HttpGet("wish-list")]
        [Authorize]
        public async Task<IActionResult> GetWishList()
        {
            Guid? wishListId = User.GetWishListId();

            if (wishListId == null)
            {
                _logger.LogError($"Не найден wish_list_id в claim {wishListId}");
                return Unauthorized();
            }
             

            GetWishListResponse response = await _mediator.Send(new GetWishListQuery((Guid)wishListId));
            return Ok(response);
        }
    }
}
