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
    public class GetCouponQueryHandler : IRequestHandler<GetCouponQuery, IEnumerable<CouponViewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCouponQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CouponViewDto>> Handle(GetCouponQuery request, CancellationToken cancellationToken)
        {
            var coupon=await _unitOfWork.CouponRepository.GetAllAsync();
            var couponView= _mapper.Map<IEnumerable<CouponViewDto>>(coupon);
            
            return couponView;
        }
    }
}
