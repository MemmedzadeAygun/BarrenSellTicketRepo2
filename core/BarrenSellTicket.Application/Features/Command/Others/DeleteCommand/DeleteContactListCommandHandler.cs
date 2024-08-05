using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others.DeleteCommand
{
    public class DeleteContactListCommandHandler : IRequestHandler<DeleteContactListCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        public DeleteContactListCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteContactListCommand request, CancellationToken cancellationToken)
        {
            var contactList = await _unitOfWork.ContactListRepository.GetByIdAsync(request.Id);
            if (contactList is null)
            {
                throw new SellTicketException("Id not found");
            }

            _unitOfWork.ContactListRepository.Remove(contactList);
            await _unitOfWork.Commit();
        }
    }
}
