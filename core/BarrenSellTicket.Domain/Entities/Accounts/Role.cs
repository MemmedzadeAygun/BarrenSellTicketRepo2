using BarrenSellTicket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Accounts
{
    public class Role:UserBaseEntity
    {
        public string RoleName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
