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
    public class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteBankAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = await _unitOfWork.BankAccountRepository.GetByIdAsync(request.Id);
            if (bankAccount == null)
            {
                throw new SellTicketException("Id not found");
            }

            _unitOfWork.BankAccountRepository.Remove(bankAccount);
            await _unitOfWork.Commit();
        }
    }
}
