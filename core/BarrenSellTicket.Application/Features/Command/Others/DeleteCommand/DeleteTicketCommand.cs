using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others.DeleteCommand
{
    public class DeleteTicketCommand:IMapTo<Ticket>,IRequest
    {
        public int Id { get; set; }
    }
}
