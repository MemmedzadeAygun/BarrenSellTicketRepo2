using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Dtos
{
    public class TicketDto:IMapTo<Ticket>
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int AvailableCount { get; set; }
        public EventViewDto Event { get; set; }
    }
}
