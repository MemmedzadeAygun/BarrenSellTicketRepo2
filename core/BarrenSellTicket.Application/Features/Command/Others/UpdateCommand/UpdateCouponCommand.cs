using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others.UpdateCommand
{
    public class UpdateCouponCommand:IMapTo<Coupon>,IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public DateTime DiscountEnd { get; set; }
        public DateTime Time { get; set; }
    }
}
