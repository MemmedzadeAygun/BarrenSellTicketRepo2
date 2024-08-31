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
    public class GetEventOrganizerIdQueryHandler : IRequestHandler<GetEventOrganizerIdQuery, List<EventDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEventOrganizerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<EventDto>> Handle(GetEventOrganizerIdQuery request, CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.EventRepository.GetEventsByOrganizerId(request.CreatedId);
            if (events is null)
            {
                return null;
            }

            var eventDtos = events.Select(eventEntity =>
            {
                var eventDto = new EventDto
                {
                    Id=eventEntity.Id,
                    Name=eventEntity.Name,
                    EventDate=eventEntity.EventDate,
                    Duration=eventEntity.Duration,
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
