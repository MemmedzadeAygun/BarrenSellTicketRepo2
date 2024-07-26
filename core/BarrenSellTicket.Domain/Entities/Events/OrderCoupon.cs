using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Events
{
    public class OrderCoupon
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }
    }
}
