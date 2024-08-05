using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Accounts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Register
{
    public class UpdateRoleCommand:IMapTo<Role>,IRequest
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
