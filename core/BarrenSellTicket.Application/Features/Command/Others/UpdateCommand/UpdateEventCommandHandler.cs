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
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var events = await _unitOfWork.EventRepository.GetByIdAsync(request.Id);
            if (events is null)
            {
                throw new ArgumentException("Event not found");
            }

            _mapper.Map(request, events);

            if (events.OrganizerDetailId!=request.OrganizerDetailId)
            {
                var organizerDetail = await _unitOfWork.OrganizerDetailRepository.GetByIdAsync(request.OrganizerDetailId);
                if (organizerDetail==null)
                {
                    throw new ArgumentException("Organizer detail not found");
                }

                events.OrganizerDetailId = request.OrganizerDetailId;
            }

            if (request.Address!=null)
            {
                var address = events.Address ?? new Address();
                _mapper.Map(request.Address, address);

                if (events.Address==null)
                {
                    events.Address = address;
                    await _unitOfWork.AddressRepository.AddAsync(address);
                }
                else
                {
                    _unitOfWork.AddressRepository.Update(address);
                }

                _unitOfWork.EventRepository.Update(events);
                await _unitOfWork.Commit();
            }
        }
    }
}
