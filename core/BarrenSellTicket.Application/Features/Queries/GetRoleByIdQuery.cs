using BarrenSellTicket.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Queries
{
    public class GetRoleByIdQuery:IRequest<RoleViewDto>
    {
        public int roleId { get; set; }
    }
}
