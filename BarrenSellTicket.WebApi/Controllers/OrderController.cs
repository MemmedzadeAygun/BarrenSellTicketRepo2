using BarrenSellTicket.Application.Features.Command.Others;
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
    }
}
