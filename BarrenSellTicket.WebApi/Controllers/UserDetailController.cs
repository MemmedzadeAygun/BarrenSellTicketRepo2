using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarrenSellTicket.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserDetailController : ApiControllerBase
    {
        public UserDetailController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ActionName("userDetails")]
        public async Task<ActionResult<ApiResponseModel<List<CustomerDto>>>> GetUsers()
        {
            var userDetails = await _mediator.Send(new GetUserDetailQuery());

            return await SuccessResult("Data selected successfully", userDetails);
        }
    }
}
