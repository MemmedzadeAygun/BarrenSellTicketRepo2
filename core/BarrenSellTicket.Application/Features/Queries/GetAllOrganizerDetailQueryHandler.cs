using AutoMapper;
using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
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
    public class GetAllOrganizerDetailQueryHandler : IRequestHandler<GetAllOrganizerDetailQuery, List<OrganizerDetailViewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllOrganizerDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<OrganizerDetailViewDto>> Handle(GetAllOrganizerDetailQuery request, CancellationToken cancellationToken)
        {
            var organizerDetails = await _unitOfWork.OrganizerDetailRepository.GetAll();

            if (organizerDetails is null)
            {
                throw new NullReferenceException("organizer is null");
            }

            var organizerDtos = new List<OrganizerDetailViewDto>();

            foreach (var organizerDetail in organizerDetails)
            {
                var organizerDto = new OrganizerDetailViewDto
                {
                    Name = organizerDetail.Name,
                    About = organizerDetail.About,
                    Phone = organizerDetail.Phone,

                    Address=new AddressDto
                    {
                        Country=organizerDetail.Address.Country,
                        City=organizerDetail.Address.City,
                        Addres=organizerDetail.Address.Addres
                    },
                   
                };

                if (!string.IsNullOrEmpty(organizerDetail.ProfileImage.ImageUrl))
                {
                    string imagePath = organizerDetail.ProfileImage.ImageUrl;

                    if (File.Exists(imagePath))
                    {
                        string imageBase64 = ImageHelper.ImageToBase64(imagePath);
                        organizerDto.Image = new ImageDto
                        {
                            ImageUrl = $"data:image/jpeg;base64,{imageBase64}",
                            Description = organizerDetail.ProfileImage.Description
                        };
                    }
                    else
                    {
                        organizerDto.Image = new ImageDto
                        {
                            ImageUrl = null,
                            Description = organizerDetail.ProfileImage.Description
                        };
                    }
                }
                else
                {
                    organizerDto.Image = new ImageDto
                    {
                        ImageUrl = null,
                        Description = organizerDetail.ProfileImage.Description
                    };
                }

                organizerDtos.Add(organizerDto);
            }
            return organizerDtos;
        }
    }
}
