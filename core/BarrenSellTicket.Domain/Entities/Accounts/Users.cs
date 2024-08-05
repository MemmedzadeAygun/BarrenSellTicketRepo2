using BarrenSellTicket.Domain.Entities.Common;
using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Accounts
{
    public class Users:UserBaseEntity
    {
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<UserRole?> UserRoles { get; set; }=new HashSet<UserRole?>();
        public ICollection<ContactList> ContactLists { get; set; }
        public OrganizerDetail OrganizerDetail { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }

}
