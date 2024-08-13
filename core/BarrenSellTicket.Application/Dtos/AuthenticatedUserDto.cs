using BarrenSellTicket.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Dtos
{
    public class AuthenticatedUserDto
    {
        public string Token { get; set; }
        public UserType Type { get; set; }
    }
}
