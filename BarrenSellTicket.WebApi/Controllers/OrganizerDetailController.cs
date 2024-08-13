using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Features.Command.Others;
using BarrenSellTicket.Application.Features.Command.Others.UpdateCommand;
using BarrenSellTicket.Application.Features.Queries;
using BarrenSellTicket.Application.Interfaces;
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
    public class OrganizerDetailController : ApiControllerBase
    {

        public OrganizerDetailController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ActionName("organizerDetail")]
        public async Task<ActionResult<ApiResponseModel<string>>> AddOrganizerDetail(AddOrganizerDetailCommand command)
        {
            if (command is null)
            {
                return await BadRequestResult<string>("Invalid request data.");
            }
            await _mediator.Send(command);
            return await SuccessResult<string>("OrganizerDetail added successfully");
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizerDetail>> GetByIdOrganizerDetail(int id)
        {
            var query = new GetOrganizerDetailQuery { organizerDetailId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }



        [HttpPut("{id}/image")]
        public async Task<ActionResult<ApiResponseModel<string>>> UpdateOrganizerImage(int id, IFormFile imageFile)
        {
            if (imageFile == null)
            {
                return BadRequest("Image file is not provided");
            }

            var command = new UpdateOrganizerİmageCommand
            {
                Id = id,
                ImageFile=imageFile
            };

            await _mediator.Send(command);

            return await SuccessResult<string>("Image update successfully");
        }



        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> UpdateOrganizerDetail(int id,UpdateOrganizerDetailCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch");
            }

            await _mediator.Send(command);
            return await SuccessResult<string>("OrganizerDetail update successfully");
        }


        [HttpGet]
        [ActionName("organizerDetail")]
        public async Task<ActionResult<ApiResponseModel<List<OrganizerDetailViewDto>>>> GetOrganizerDetails()
        {
            var organizerdetail = await _mediator.Send(new GetAllOrganizerDetailQuery());

            return await SuccessResult("OrganizerDetail data is selected", organizerdetail);
        }
    }
}
