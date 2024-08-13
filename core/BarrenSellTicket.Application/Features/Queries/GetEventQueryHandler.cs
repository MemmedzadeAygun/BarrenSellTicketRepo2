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
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, List<EventViewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetEventQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EventViewDto>> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.EventRepository.GetAll();
            if (events == null)
            {
                throw new NullReferenceException("events is null");
            }


            var eventDtos =new List<EventViewDto>();
            foreach (var eventEntity in events)
            {

                if (eventEntity.EventCategory == null)
                {
                    throw new NullReferenceException("eventEntity.Category is null");
                }

                var eventDto = new EventViewDto
                {
                    Name = eventEntity.Name,
                    EventDate = eventEntity.EventDate,
                    BeginTime = eventEntity.BeginTime,
                    EndTime = eventEntity.EndTime,
                    Duration = eventEntity.Duration,
                    Description = eventEntity.Description,

                    Address=new AddressDto
                    {
                        Country=eventEntity.Address.Country,
                        City=eventEntity.Address.City,
                        Addres=eventEntity.Address.Addres
                    },
                    Type=new TypeDto
                    {
                        Name=eventEntity.EventType.Name,
                    },
                    Category=new CategoryDto
                    {
                        Name=eventEntity.EventCategory.Name
                    }
                };

                eventDtos.Add(eventDto);
            }
            return eventDtos;
        }
    }
}
