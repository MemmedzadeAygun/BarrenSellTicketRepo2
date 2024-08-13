using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Infrastructure.Helper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Queries
{
    public class GetEventImageQueryHandler : IRequestHandler<GetEventImageQuery, List<EventImageDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEventImageQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<EventImageDto>> Handle(GetEventImageQuery request, CancellationToken cancellationToken)
        {
            var eventImages = await _unitOfWork.EventRepository.GetAllEventImage();
            if (eventImages == null)
            {
                throw new NullReferenceException("event is null");
            }

            var eventImageDtos= new List<EventImageDto>();

            foreach (var eventImage in eventImages)
            {
                var eventImageDto = new EventImageDto();

                if (!string.IsNullOrEmpty(eventImage.Image.ImageUrl))
                {
                    string imagePath = eventImage.Image.ImageUrl;

                    if (File.Exists(imagePath))
                    {
                        string imageBase64 = ImageHelper.ImageToBase64(imagePath);
                        eventImageDto.Image = new ImageDto
                        {
                            ImageUrl = $"data:image/jpeg;base64,{imageBase64}",
                            Description = eventImage.Image.Description
                        };
                    }
                    else
                    {
                        eventImageDto.Image = new ImageDto
                        {
                            ImageUrl = null,
                            Description = eventImage.Image.Description
                        };
                    }
                }
                else
                {
                    eventImageDto.Image = new ImageDto
                    {
                        ImageUrl = null,
                        Description = eventImage.Image.Description
                    };
                }

                eventImageDtos.Add(eventImageDto);
            }
            return eventImageDtos;
        }
    }
}
