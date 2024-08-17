using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Features.Command.Others;
using BarrenSellTicket.Application.Features.Command.Others.DeleteCommand;
using BarrenSellTicket.Application.Features.Command.Others.UpdateCommand;
using BarrenSellTicket.Application.Features.Queries;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BarrenSellTicket.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TicketController : ApiControllerBase
    {
        public TicketController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ActionName("ticket")]
        public async Task<ActionResult<ApiResponseModel<string>>> AddTicket(AddTicketCommand command)
        {
            if (command is null)
            {
                return await BadRequestResult<string>("Invalid request result");
            }
            await _mediator.Send(command);
            return await SuccessResult<string>("Ticket added successfully");
        }

        [HttpGet]
        [ActionName("tickets")]
        public async Task<ActionResult<ApiResponseModel<List<TicketDto>>>> getTickets()
        {
            var tickets = await _mediator.Send(new GetAllTicketQuery());

            return await SuccessResult("Data selected successfully", tickets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicketById(int id)
        {
            var query = new GetTicketByIdQuery
            {
                ticketId = id
            };

            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> UpdateTicket(int id, [FromBody] UpdateTicketCommand command)
        {
            if (id!=command.Id)
            {
                return BadRequest("Id mismatch");
            }

            await _mediator.Send(command);

            return await SuccessResult<string>("Ticket update successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> DeleteTicket(int id)
        {
            var command = new DeleteTicketCommand
            {
                Id = id
            };

            try
            {
                await _mediator.Send(command);
                return await SuccessResult<string>("Ticket delete successfully");
            }
            catch (KeyNotFoundException ex)
            {
                throw new SellTicketException("Id not found");
            }
            catch(Exception ex)
            {
                return await InternalServerErrorResult<string>(ex.Message);
            }
        }
    }
}
