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
    public class GetOrderByCreatedIdQueryHandler : IRequestHandler<GetOrderByCreatedIdQuery, List<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetOrderByCreatedIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<OrderDto>> Handle(GetOrderByCreatedIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByCreatedId(request.CreatedId);

            return orders;
        }
    }
}
