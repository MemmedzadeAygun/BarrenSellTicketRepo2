using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Features.Queries;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarrenSellTicket.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ApiControllerBase
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ActionName("categories")]
        public async Task<ActionResult<ApiResponseModel<List<CategoryDto>>>> GetCategories()
        {
            var categories=await _mediator.Send(new GetAllCategoryQuery());

            return await SuccessResult("Data selected successfully", categories);
        }

    }
}
