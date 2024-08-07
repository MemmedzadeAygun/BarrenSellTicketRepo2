using AutoMapper;
using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Extensions;
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
    public class GetOrganizerDetailByIdHandler : IRequestHandler<GetOrganizerDetailQuery,OrganizerDetailViewDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetOrganizerDetailByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrganizerDetailViewDto> Handle(GetOrganizerDetailQuery request, CancellationToken cancellationToken)
        {
            var organizerDetail = await _unitOfWork.OrganizerDetailRepository.GetById(request.organizerDetailId);

            if (organizerDetail == null)
            {
                return null; 
            }

            var organizerDetailDto = _mapper.Map<OrganizerDetailViewDto>(organizerDetail);
            organizerDetailDto.Address = _mapper.Map<AddressDto>(organizerDetail.Address);


            if (!string.IsNullOrEmpty(organizerDetail.ProfileImage.ImageUrl))
            {
                string imagePath = organizerDetail.ProfileImage.ImageUrl;

                if (File.Exists(imagePath))
                {
                    string imageBase64 = ImageHelper.ImageToBase64(imagePath);
                    organizerDetailDto.Image = new ImageDto
                    {
                        ImageUrl = $"data:image/jpeg;base64,{imageBase64}",
                        Description = organizerDetail.ProfileImage.Description
                    };
                }
                else
                {
                    organizerDetailDto.Image = new ImageDto
                    {
                        ImageUrl = null,
                        Description = organizerDetail.ProfileImage.Description
                    };
                }
            }
            else
            {
                organizerDetailDto.Image = new ImageDto
                {
                    ImageUrl = null,
                    Description = organizerDetail.ProfileImage.Description
                };
            }

            return organizerDetailDto;
        }
    }
}
