using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Features.Command.Others;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarrenSellTicket.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganizerDetailController : ApiControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizerDetailController(IMediator mediator, IUnitOfWork unitOfWork) : base(mediator)
        {
            _unitOfWork = unitOfWork;
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
            var organizerDetail = await _unitOfWork.OrganizerDetailRepository.GetByIdAsync(id);
            if (organizerDetail is null)
            {
                return NotFound();
            }

            return Ok(organizerDetail);
        }
    }
}
