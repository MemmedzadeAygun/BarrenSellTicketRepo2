using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Mappers.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Queries
{
    public class GetEventTypeIdQuery:IRequest<EventViewDto>
    {
        public int TypeId { get; set; }
    }
}
