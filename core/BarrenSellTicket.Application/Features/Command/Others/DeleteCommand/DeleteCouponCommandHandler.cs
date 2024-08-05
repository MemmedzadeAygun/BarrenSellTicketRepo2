using AutoMapper;
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
    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteCouponCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = await _unitOfWork.CouponRepository.GetByIdAsync(request.Id);
            if (coupon is null)
            {
                throw new SellTicketException("Id not found");
            }

            _unitOfWork.CouponRepository.Remove(coupon);
            await _unitOfWork.Commit();
        }
    }
}
