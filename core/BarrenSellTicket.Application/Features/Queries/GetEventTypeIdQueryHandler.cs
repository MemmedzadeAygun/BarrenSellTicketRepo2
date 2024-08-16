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
    public class GetEventTypeIdQueryHandler : IRequestHandler<GetEventTypeIdQuery, EventViewDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetEventTypeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<EventViewDto> Handle(GetEventTypeIdQuery request, CancellationToken cancellationToken)
        {
            var getEvent = await _unitOfWork.EventRepository.GetEventTypeId(request.TypeId);
            if (getEvent is null)
            {
                return null;
            }

            var eventDto = _mapper.Map<EventViewDto>(getEvent);

            eventDto.Category = _mapper.Map<CategoryDto>(getEvent.EventCategory);
            eventDto.Type=_mapper.Map<TypeDto>(getEvent.EventType);
            eventDto.Address = _mapper.Map<AddressDto>(getEvent.Address);

            return eventDto;
        }
    }
}
