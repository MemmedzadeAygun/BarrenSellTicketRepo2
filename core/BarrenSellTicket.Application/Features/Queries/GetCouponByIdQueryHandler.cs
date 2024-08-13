using AutoMapper;
using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Queries
{
    public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, CouponViewDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCouponByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CouponViewDto> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _unitOfWork.CouponRepository.GetByIdAsync(request.couponId);
            if (coupon is null)
            {
                return null;
            }

            var couponDto=_mapper.Map<CouponViewDto>(coupon);
            return couponDto;
        }
    }
}
