using AutoMapper;
using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using BarrenSellTicket.Infrastructure.Helper;
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

            var eventDto = new EventViewDto
            {
                Id = events.Id,
                Name = events.Name,
                EventDate = events.EventDate,
                BeginTime = events.BeginTime,
                EndTime = events.EndTime,
                Duration = events.Duration,
                Description = events.Description,

                Address = new AddressDto
                {
                    //Id = events.Address.Id,
                    Country = events.Address.Country,
                    City = events.Address.City,
                    Addres = events.Address.Addres
                },

                Category = new CategoryDto
                {
                    Id = events.EventCategory.Id,
                    Name = events.EventCategory.Name
                },

                Type = new TypeDto
                {
                    Name = events.EventType.Name
                },

                Tickets = events.Tickets.Select(ticket => new TicketViewDto
                {
                    Id = ticket.Id,
                    Price = ticket.Price,
                    AvailableCount = ticket.AvailableCount
                }).ToList(),
            };

            if (events.Image!=null)
            {
                string imageUrl = null;
                if (!string.IsNullOrEmpty(events.Image.ImageUrl) && File.Exists(events.Image.ImageUrl))
                {
                    string imageBase64 = ImageHelper.ImageToBase64(events.Image.ImageUrl);
                    imageUrl = $"data:image/jpeg;base64,{imageBase64}";
                }

                eventDto.Image = new ImageDto
                {
                    ImageUrl = imageUrl,
                    Description = events.Image.Description
                };
            }

            //var timeremaining = events.EventDate - DateTime.Now;

            //if (timeremaining.TotalSeconds<=0)
            //{
            //    timeremaining = TimeSpan.Zero;
            //}

            //eventDto.CountDown = new EventDateCountDownDto
            //{
            //    Days = timeremaining.Days,
            //    Hours = timeremaining.Hours,
            //    Minutes = timeremaining.Minutes,
            //    Seconds = timeremaining.Seconds
            //};

            return eventDto;
        }
    }
}
