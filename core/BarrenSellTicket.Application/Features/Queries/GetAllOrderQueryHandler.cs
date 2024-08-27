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
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, List<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllOrderQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<OrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.OrderRepository.GetOrdersWithUserDetailsAsync(); 
        }
    }
}
