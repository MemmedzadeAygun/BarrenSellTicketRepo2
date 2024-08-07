using BarrenSellTicket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Events
{
    public class Image:BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; } = string.Empty;
        public Event Event { get; set; }
        public OrganizerDetail OrganizerDetail { get; set; }
    }
}
