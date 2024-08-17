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
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var existingTicket = await _unitOfWork.TicketRepository.GetByIdAsync(request.Id);
            if (existingTicket != null)
            {
                _mapper.Map(request, existingTicket);
                _unitOfWork.TicketRepository.Update(existingTicket);
            }
            else
            {
                var newTicket= _mapper.Map<Ticket>(request);
                await _unitOfWork.TicketRepository.AddAsync(newTicket);
            }

            await _unitOfWork.Commit();
        }
    }
}
