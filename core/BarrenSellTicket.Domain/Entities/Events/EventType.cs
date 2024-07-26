using BarrenSellTicket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Events
{
    public class EventType:BaseEntity
    {
        public EventType()
        {
            Events = new HashSet<Event>();
        }
        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
