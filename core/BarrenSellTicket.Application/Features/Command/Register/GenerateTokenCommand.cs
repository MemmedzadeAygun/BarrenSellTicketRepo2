using BarrenSellTicket.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Register
{
    public class GenerateTokenCommand:IRequest<AuthenticatedUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
