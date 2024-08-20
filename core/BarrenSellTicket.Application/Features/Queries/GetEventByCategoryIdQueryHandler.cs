using AutoMapper;
using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Infrastructure.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Queries
{
    public class GetEventByCategoryIdQueryHandler : IRequestHandler<GetEventByCategoryIdQuery, List<EventViewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetEventByCategoryIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EventViewDto>> Handle(GetEventByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.EventRepository.GetEventByCategoryId(request.CategoryId);
            if (events is null)
            {
                return new List<EventViewDto>();
            }

            var eventDtos = events.Select(eventEntity => 
            {
                var eventDto = new EventViewDto
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
                        //Id = eventEntity.Address.Id,
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

                    Tickets = eventEntity.Tickets?.Select(t => new TicketViewDto
                    {
                        Id = t.Id,
                        Price = t.Price,
                        AvailableCount = t.AvailableCount
                    }).ToList()
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

                return eventDto;

            }).ToList();

            return eventDtos;
        }
        
    }
}
