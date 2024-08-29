using AutoMapper;
using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Infrastructure.Helper;
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
            var query =  _unitOfWork.EventRepository.GetAll();

            int pageNumber = request.PageNumber > 0 ? request.PageNumber : 1;
            int pageSize = request.PageSize > 0 ? request.PageSize : 10;

            var pagedQuery = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var events = pagedQuery.ToList();

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
                    Id=eventEntity.Id,
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
                        Id=eventEntity.EventCategory.Id,
                        Name=eventEntity.EventCategory.Name
                    },
                    Tickets= eventEntity.Tickets.Select(t=>new TicketViewDto
                    {
                        Id=t.Id,
                        Price=t.Price,
                        AvailableCount=t.AvailableCount
                    }).ToList(),
                };

                if (eventEntity.Image != null)
                {
                    string imageUrl = null;
                    if (!string.IsNullOrEmpty(eventEntity.Image.ImageUrl) && File.Exists(eventEntity.Image.ImageUrl))
                    {
                        string imageBase64 = ImageHelper.ImageToBase64(eventEntity.Image.ImageUrl);
                        imageUrl = $"data:image/jpeg;base64,{imageBase64}";
                    }

                    eventDto.Image = new ImageDto
                    {
                        ImageUrl = imageUrl,
                        Description = eventEntity.Image.Description
                    };
                }

                eventDtos.Add(eventDto);
            }
            return eventDtos;
        }
    }
}
