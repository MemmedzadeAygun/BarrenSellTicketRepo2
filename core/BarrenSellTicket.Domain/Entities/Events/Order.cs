using BarrenSellTicket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Events
{
    public class Order:BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public int TicketId { get; set; }
        public int Quantity { get; set; }
        public Ticket Ticket { get; set; }
        public ICollection<OrderCoupon> OrderCoupons { get; set; }
        public ICollection<Payouts> Payouts { get; set; }
    }
}
