using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Dtos
{
    public class EventViewDto:IMapTo<Event>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Duration { get; set; }
        public string Description { get; set; }
        public AddressDto Address { get; set; }
        public TypeDto Type { get; set; }
        public CategoryDto Category { get; set; }
        public List<TicketViewDto> Tickets { get; set; }
        public ImageDto Image { get; set; }
    }
}
