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
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTicketCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _unitOfWork.TicketRepository.GetByIdAsync(request.Id);
            if (ticket is null)
            {
                throw new SellTicketException("Id not found");
            }

            _unitOfWork.TicketRepository.Remove(ticket);
            await _unitOfWork.Commit();
        }
    }
}
