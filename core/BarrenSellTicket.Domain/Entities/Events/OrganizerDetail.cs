using BarrenSellTicket.Domain.Entities.Accounts;
using BarrenSellTicket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Events
{
    public class OrganizerDetail:BaseEntity
    {
        public OrganizerDetail()
        {
            Events = new HashSet<Event>();
        }
        public string Name { get; set; }
        public string About { get; set; }
        public string Phone { get; set; }
        public int? ImageId { get; set; }
        public Image ProfileImage { get; set; }
        public ICollection<Event> Events { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
    }
}
