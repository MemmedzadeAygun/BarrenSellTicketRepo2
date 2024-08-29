using AutoMapper;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others
{
    public class AddTicketCommandHandler : IRequestHandler<AddTicketCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task Handle(AddTicketCommand request, CancellationToken cancellationToken)
        {
            var eventId = int.Parse(_httpContextAccessor.HttpContext.Request.RouteValues["eventId"].ToString());

            var ticket = _mapper.Map<Ticket>(request);
            ticket.EventId = eventId;

            await _unitOfWork.TicketRepository.AddAsync(ticket);
            await _unitOfWork.Commit();
        }
    }
}
