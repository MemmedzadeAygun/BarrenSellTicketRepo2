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
    public class GetEventTypeIdQueryHandler : IRequestHandler<GetEventTypeIdQuery, List<EventViewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetEventTypeIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<EventViewDto>> Handle(GetEventTypeIdQuery request, CancellationToken cancellationToken)
        {
            var getEvent = await _unitOfWork.EventRepository.GetEventTypeId(request.TypeId);
            if (getEvent is null)
            {
                return new List<EventViewDto>();
            }

            var eventDtos = getEvent.Select(eventEntity => new EventViewDto
            {
                Id = eventEntity.Id,
                Name = eventEntity.Name,
                EventDate = eventEntity.EventDate,
                BeginTime = eventEntity.BeginTime,
                EndTime = eventEntity.EndTime,
                Duration = eventEntity.Duration,
                Description = eventEntity.Description,

                Address = new AddressDto
                {
                    Id = eventEntity.Address.Id,
                    Country = eventEntity.Address.Country,
                    City = eventEntity.Address.City,
                    Addres = eventEntity.Address.Addres
                },

                Type = new TypeDto
                {
                    Name = eventEntity.EventType.Name
                },

                Category = new CategoryDto
                {
                    Id = eventEntity.EventCategory.Id,
                    Name = eventEntity.EventCategory.Name
                },

            }).ToList();

            return eventDtos;

        }
    }
}
