using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Dtos
{
    public class OrderDto:IMapTo<Order>
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public CustomerDto UserDetails { get; set; }
        public TicketDto Ticket { get; set; }
    }
}
