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
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteEventCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.EventRepository.GetByIdAsync(request.Id);
            if (events is null)
            {
                throw new SellTicketException("Event not found");
            }

            _unitOfWork.EventRepository.Remove(events);
            await _unitOfWork.Commit();
        }
    }
}
