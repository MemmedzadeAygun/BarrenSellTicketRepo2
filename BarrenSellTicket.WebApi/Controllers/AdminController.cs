using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Features.Command.Register;
using BarrenSellTicket.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarrenSellTicket.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ApiControllerBase
    {
        public AdminController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ActionName("role")]
        public async Task<ActionResult<ApiResponseModel<string>>> CreateRole(CreateRoleCommand command)
        {
            await _mediator.Send(command);
            return await SuccessResult<string>("Role added successfully");
        }

        [HttpGet]
        [ActionName("role")]
        public async Task<ActionResult<ApiResponseModel<IEnumerable<RoleViewDto>>>> GetRoleAsync()
        {
            var role = await _mediator.Send(new GetRoleQuery());

            return await SuccessResult("Role data is selected", role);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> UpdateRole(int id,[FromBody] UpdateRoleCommand command)
        {
            if (id!=command.Id)
            {
                return BadRequest("Id mismatch");
            }

            await _mediator.Send(command);
            return await SuccessResult<string>("Role updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> DeleteRole(int id)
        {
            var command = new DeleteRoleCommand
            {
                Id = id
            };

            try
            {
                await _mediator.Send(command);
                return await SuccessResult<string>("Role deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return await NotFoundResult<string>(ex.Message);
            }
            catch(Exception ex)
            {
                return await InternalServerErrorResult<string>(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("assignRole")]
        public async Task<ActionResult<ApiResponseModel<string>>> CreateRolesToUser(CreateUserRoleCommand command)
        {
            await _mediator.Send(command);
            return await SuccessResult<string>("The user has been assigned a role");
        }
    }
}
