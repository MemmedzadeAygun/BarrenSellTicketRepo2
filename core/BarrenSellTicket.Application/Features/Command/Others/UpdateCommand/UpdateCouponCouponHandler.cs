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
    public class UpdateCouponCouponHandler : IRequestHandler<UpdateCouponCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCouponCouponHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var existingCoupon = await _unitOfWork.CouponRepository.GetByIdAsync(request.Id);
            if (existingCoupon!=null)
            {
                _mapper.Map(request, existingCoupon);
                _unitOfWork.CouponRepository.Update(existingCoupon);
            }
            else
            {
                var newCoupon=_mapper.Map<Coupon>(request);
                await _unitOfWork.CouponRepository.AddAsync(newCoupon);
            }

            await _unitOfWork.Commit();
        }
    }
}
