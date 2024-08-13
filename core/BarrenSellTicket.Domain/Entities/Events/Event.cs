using BarrenSellTicket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Events
{
    public class Event:BaseEntity
    {
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Duration { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }
        public int EventCategoryId { get; set; }
        public EventCategory EventCategory { get; set; }
        public int OrganizerDetailId { get; set; }
        public int? ImageId { get; set; }
        public Image Image { get; set; }
        public OrganizerDetail OrganizerDetail { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
