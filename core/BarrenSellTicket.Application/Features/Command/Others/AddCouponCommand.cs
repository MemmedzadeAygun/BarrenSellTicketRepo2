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
    public class AddCouponCommand:IMapTo<Coupon>,IRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public DateTime DiscountEnd { get; set; }
        public DateTime Time { get; set; }
    }

   
}
