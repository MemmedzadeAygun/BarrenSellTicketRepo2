using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Features.Command.Register;
using BarrenSellTicket.Domain.Entities.Accounts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarrenSellTicket.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ActionName("SignUp")]
        public async Task<ActionResult<ApiResponseModel<AuthenticatedUserDto>>> Register(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return await SuccessResult("Token generated successfully", result);
           
        }

        [HttpPost]
        [ActionName("SignIn")]
        public async Task<ActionResult<ApiResponseModel<AuthenticatedUserDto>>> Login(GenerateTokenCommand command)
        {
            var result = await _mediator.Send(command);

            //if (result.Type == UserType.Admin)
            //{
            //    return await SuccessResult("Admin olaraq daxil oldunuz", result);
            //}
            //else if (result.Type==UserType.Organizer)
            //{
            //    return await SuccessResult("Teskilatci olaraq daxil oldunuz", result);
            //}
            //else if (result.Type==UserType.User)
            //{
            //    return await SuccessResult("Istifadeci olaraq daxil oldunuz", result);
            //}

            return await SuccessResult("Token generated successfully", result);

            //return Unauthorized("Selahiyyetiniz yoxdur");
        }
    }
}
