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
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventViewDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetEventByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EventViewDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.EventRepository.GetById(request.Id);
            if (events is null)
            {
                return null;
            }

            var eventsDto = _mapper.Map<EventViewDto>(events);

            eventsDto.Category = _mapper.Map<CategoryDto>(events.EventCategory);
            eventsDto.Type = _mapper.Map<TypeDto>(events.EventType);
            eventsDto.Address = _mapper.Map<AddressDto>(events.Address);

            return eventsDto;
        }
    }
}
