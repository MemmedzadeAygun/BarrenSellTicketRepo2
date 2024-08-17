using AutoMapper;
using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Queries
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetTicketByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _unitOfWork.TicketRepository.GetById(request.ticketId);
            if (ticket is null)
            {
                return null;
            }

            var ticketDto = _mapper.Map<TicketDto>(ticket);
            ticketDto.Event = _mapper.Map<EventViewDto>(ticket.Event);
            ticketDto.Event.Type = _mapper.Map<TypeDto>(ticket.Event.EventType);
            ticketDto.Event.Category = _mapper.Map<CategoryDto>(ticket.Event.EventCategory);
            ticketDto.Event.Address = _mapper.Map<AddressDto>(ticket.Event.Address);

            return ticketDto;
        }
    }
}
