using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Features.Command.Others;
using BarrenSellTicket.Application.Features.Command.Others.DeleteCommand;
using BarrenSellTicket.Application.Features.Command.Others.UpdateCommand;
using BarrenSellTicket.Application.Features.Queries;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarrenSellTicket.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ContactListController : ApiControllerBase
    {
        public ContactListController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ActionName("contactList")]
        public async Task<ActionResult<ApiResponseModel<string>>> AddContactList(AddContactListCommand command)
        {
            await _mediator.Send(command);
            return await SuccessResult<string>("ContactList was added");
        }

        [HttpGet]
        [ActionName("getList")]
        public async Task<ActionResult<ApiResponseModel<IEnumerable<ContactListViewDto>>>> GetContatctList()
        {
            var contactList = await _mediator.Send(new GetContactListQuery());

            return await SuccessResult("ContactList data is selected", contactList);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> UpdateContactList(int id, [FromBody] UpdateContactListCommand command)
        {
            if (id!=command.Id)
            {
                return BadRequest("Id mismatch");
            }

            await _mediator.Send(command);
            return await SuccessResult<string>("ContactList updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> DeleteContactList(int id)
        {
            var command = new DeleteContactListCommand
            {
                Id = id
            };

            try
            {
                await _mediator.Send(command);
                return await SuccessResult<string>("ContactList deleted successsfully");
            }
            catch (KeyNotFoundException ex)
            {
                return await NotFoundResult<string>(ex.Message);
            }
            catch (Exception ex)
            {
                return await InternalServerErrorResult<string>(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactList>> getContactListById(int id)
        {
            var query = new GetContactListByIdQuery { contactListId = id };
            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
