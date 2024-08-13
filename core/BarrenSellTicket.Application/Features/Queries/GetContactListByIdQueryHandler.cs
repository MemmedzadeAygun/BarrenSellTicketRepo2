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
    public class GetContactListByIdQueryHandler : IRequestHandler<GetContactListByIdQuery, ContactListViewDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetContactListByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ContactListViewDto> Handle(GetContactListByIdQuery request, CancellationToken cancellationToken)
        {
            var contactList = await _unitOfWork.ContactListRepository.GetByIdAsync(request.contactListId);
            if (contactList == null)
            {
                return null;
            }

            var contactListDto = _mapper.Map<ContactListViewDto>(contactList);
            return contactListDto;
        }
    }
}
