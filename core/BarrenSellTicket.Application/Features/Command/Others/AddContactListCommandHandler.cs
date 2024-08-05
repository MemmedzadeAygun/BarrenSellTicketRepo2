using AutoMapper;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others
{
    public class AddContactListCommandHandler : IRequestHandler<AddContactListCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddContactListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(AddContactListCommand request, CancellationToken cancellationToken)
        {
            var contactList = _mapper.Map<ContactList>(request);
            await _unitOfWork.ContactListRepository.AddAsync(contactList);
            await _unitOfWork.Commit();
        }
    }
}
