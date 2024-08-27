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
using Microsoft.AspNetCore.Mvc;

namespace BarrenSellTicket.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EventController : ApiControllerBase
    {
        public EventController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ActionName("event")]
        public async Task<ActionResult<ApiResponseModel<string>>> AddEvents(AddEventCommand command)
        {
            if (command is null)
            {
                return await BadRequestResult<string>("Invalid request data.");
            }
            await _mediator.Send(command);
            return await SuccessResult<string>("Event added successfully");

        }

        [HttpPut("{id}/image")]
        public async Task<ActionResult<ApiResponseModel<string>>> UpdateEventImage(int id,IFormFile imageFile)
        {
            if (imageFile is null)
            {
                return await BadRequestResult<string>("Image file is not provided");
            }

            var command = new UpdateEventImageCommand
            {
                Id = id,
                ImageFile = imageFile
            };

            await _mediator.Send(command);
            return await SuccessResult<string>("Image updated successfully");
        }

        [HttpGet]
        [ActionName("events")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponseModel<List<EventViewDto>>>> GetEvents(
            [FromQuery] int pageNumber=1,
            [FromQuery] int pageSize=10)
        {
            var query = new GetEventQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var events = await _mediator.Send(query);

            return await SuccessResult("Data selected successfully",events);
        }

        [HttpGet]
        [ActionName("eventImages")]
        public async Task<ActionResult<ApiResponseModel<List<EventImageDto>>>> GetEventImages()
        {
            var eventImages = await _mediator.Send(new GetEventImageQuery());

            if (eventImages is null)
            {
                return NotFound("EventImage not found");
            }

            return await SuccessResult("Data selected successfully", eventImages);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> UpdateEvent(int id, [FromBody] UpdateEventCommand command)
        {
            if (id!=command.Id)
            {
                return BadRequest("Id mismatch");
            }

            await _mediator.Send(command);
            return await SuccessResult<string>("Event update successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> DeleteEvent(int id)
        {
            var command = new DeleteEventCommand
            {
                Id = id
            };

            try
            {
                await _mediator.Send(command);
                return await SuccessResult<string>("Delete event successfully");
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
        [AllowAnonymous]
        public async Task<ActionResult<Event>> GetByIdEvent(int id)
        {
            var query = new GetEventByIdQuery
            {
                Id = id
            };
            var result = await _mediator.Send(query);
            if (result==null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{typeid}")]
        public async Task<ActionResult<Event>> GetEventByTypeId(int typeid)
        {
            var query = new GetEventTypeIdQuery
            {
                TypeId = typeid
            };

            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpGet("{categoryid}")]
        [AllowAnonymous]
        public async Task<ActionResult<Event>> GetEventByCategoryId(int categoryid)
        {
            var query = new GetEventByCategoryIdQuery
            {
                CategoryId = categoryid
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
