using AutoMapper;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others.UpdateCommand
{
    public class UpdateContactListCommandHandler : IRequestHandler<UpdateContactListCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateContactListCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(UpdateContactListCommand request, CancellationToken cancellationToken)
        {
            var existingContactList = await _unitOfWork.ContactListRepository.GetByIdAsync(request.Id);
            if (existingContactList!=null)
            {
                _mapper.Map(request, existingContactList);
                _unitOfWork.ContactListRepository.Update(existingContactList);
            }
            else
            {
                var newContactList = _mapper.Map<ContactList>(request);
                await _unitOfWork.ContactListRepository.AddAsync(newContactList);
            }

            await _unitOfWork.Commit();
        }
    }
}
