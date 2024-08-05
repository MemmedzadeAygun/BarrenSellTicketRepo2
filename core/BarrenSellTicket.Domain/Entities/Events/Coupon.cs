using BarrenSellTicket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Events
{
    public class Coupon:BaseEntity
    {
        public string Name { get; set; }
        public string Code  { get; set; }
        public decimal Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public DateTime DiscountEnd { get; set; }
        public DateTime Time { get; set; }
        public ICollection<OrderCoupon> OrderCoupons { get; set; }
    }

    public enum DiscountType:int
    {
        Percent = 1  ,
        FixedPrice=2
    }
}
