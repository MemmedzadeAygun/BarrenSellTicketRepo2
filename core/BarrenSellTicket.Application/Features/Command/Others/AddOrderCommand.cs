using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others
{
    public class AddOrderCommand:IMapTo<Order>,IRequest
    {
        public int Quantity { get; set; }
        public List<string> CouponCodes { get; set; }
        public PayoutsDto Payout { get; set; }

    }
}
