using AutoMapper;
using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Queries
{
    public class GetOrganizerDetailByIdHandler : IRequestHandler<GetOrganizerDetailQuery, IEnumerable<OrganizerDetailViewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetOrganizerDetailByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<IEnumerable<OrganizerDetailViewDto>> Handle(GetOrganizerDetailQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<OrganizerDetailViewDto>> Handle(GetOrganizerDetailQuery request, CancellationToken cancellationToken)
        //{
        //    var organizerDetail = await _unitOfWork.OrganizerDetailRepository.GetByIdAsync(request.organizerDetailId);

        //    var adress = await _unitOfWork.AddressRepository.GetByIdAsync((int)organizerDetail.AddressId);

        //    var user = await _unitOfWork.UserRepository.GetUserById(organizerDetail.UserId);


        //}
    }
}
