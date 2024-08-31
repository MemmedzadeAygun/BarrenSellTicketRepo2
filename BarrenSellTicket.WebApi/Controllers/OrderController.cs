using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Features.Command.Others;
using BarrenSellTicket.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BarrenSellTicket.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        public OrderController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("add/{eventId}")]
        public async Task<ActionResult<ApiResponseModel<string>>> AddOrder(int eventId, AddOrderCommand command)
        {
            if (command is null)
            {
                return await BadRequestResult<string>("Invalid request data.");
            }


            await _mediator.Send(command);
            return await SuccessResult<string>("Order added successfully");
        }

        [HttpGet]
        [ActionName("orders")]
        public async Task<ActionResult<ApiResponseModel<List<OrderDto>>>> GetOrders()
        {
            var orders = await _mediator.Send(new GetAllOrderQuery());
            return await SuccessResult("Order's data selected successfully", orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseModel<OrderDto>>> GetOrderById(int id)
        {
            var query = new GetOrderByIdQuery
            {
                OrderId = id
            };

            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }

            return await SuccessResult("Order data selected successfully", result);
        }

        [HttpGet("{createdid}")]
        public async Task<ActionResult<OrderDto>> GetOrderByCreatedId(int createdid)
        {
            var query = new GetOrderByCreatedIdQuery
            {
                CreatedId = createdid
            };

            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
