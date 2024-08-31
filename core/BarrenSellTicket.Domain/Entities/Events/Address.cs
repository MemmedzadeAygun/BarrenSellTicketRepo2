using BarrenSellTicket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Events
{
    public class Address:BaseEntity
    {
        public Address()
        {
            Events = new HashSet<Event>();
        }
        public string Country { get; set; }
        public string City { get; set; }
        public string Addres { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<OrganizerDetail> OrganizerDetails { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
