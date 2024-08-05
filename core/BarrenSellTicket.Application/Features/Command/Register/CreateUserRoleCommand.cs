using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Register
{
    public class CreateUserRoleCommand:IRequest
    {
        public int UserId { get; set; }
        public ICollection<int> RoleIds { get; set; }
    }
}
