using AutoMapper;
using BarrenSellTicket.Application.Extensions;
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
    public class UpdateOrganizerDetailCommandHandler : IRequestHandler<UpdateOrganizerDetailCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateOrganizerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(UpdateOrganizerDetailCommand request, CancellationToken cancellationToken)
        {
            var organizerDetail = await _unitOfWork.OrganizerDetailRepository.GetById(request.Id);
            if (organizerDetail==null)
            {
               throw new ArgumentException("Organizer not found");
            }

            _mapper.Map(request, organizerDetail);

            if (request.Address!=null)
            {
                var address = organizerDetail.Address ?? new Address();
                _mapper.Map(request.Address, address);

                if(organizerDetail.Address==null)
                {
                    organizerDetail.Address = address;
                    await _unitOfWork.AddressRepository.AddAsync(address);
                }
                else
                {
                   _unitOfWork.AddressRepository.Update(address);
                }

                 _unitOfWork.OrganizerDetailRepository.Update(organizerDetail);
                 await _unitOfWork.Commit();
            }
        }
    }
}
