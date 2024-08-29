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
    public class AddTicketCommand:IMapTo<Ticket>,IRequest
    {
        public decimal Price { get; set; }
        public int AvailableCount { get; set; }
        //public int EventId { get; set; }
    }
}
