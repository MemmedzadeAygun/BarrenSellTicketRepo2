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
    public class AddTicketCommandHandler : IRequestHandler<AddTicketCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(AddTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = _mapper.Map<Ticket>(request);
            await _unitOfWork.TicketRepository.AddAsync(ticket);
            await _unitOfWork.Commit();
        }
    }
}
