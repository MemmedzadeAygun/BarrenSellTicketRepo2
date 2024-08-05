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
    public class GetContactListQueryHandler : IRequestHandler<GetContactListQuery, IEnumerable<ContactListViewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetContactListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ContactListViewDto>> Handle(GetContactListQuery request, CancellationToken cancellationToken)
        {
            var contactList = await _unitOfWork.ContactListRepository.GetAllAsync();
            var contactListView = _mapper.Map<IEnumerable<ContactListViewDto>>(contactList);

            return contactListView;
        }
    }
}
