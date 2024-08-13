using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Features.Command.Others;
using BarrenSellTicket.Application.Features.Command.Others.UpdateCommand;
using BarrenSellTicket.Application.Features.Queries;
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
        public async Task<ActionResult<ApiResponseModel<List<EventViewDto>>>> GetEvents()
        {
            var events = await _mediator.Send(new GetEventQuery());

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
    }
}
