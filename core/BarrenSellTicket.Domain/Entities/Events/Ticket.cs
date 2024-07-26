using BarrenSellTicket.Domain.Entities.Accounts;
using BarrenSellTicket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Events
{
    public class Ticket:BaseEntity
    {
        public decimal Price { get; set; }
        public int AvailableCount { get; set; }
        public int Quantity { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int? UserId { get; set; }
        public Users User { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
