using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Features.Command.Register;
using BarrenSellTicket.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Interfaces.Register
{
    public interface IRoleRepository
    {
        Task AddRole(Role role);
        Task<List<Role?>> GetRoles();
        Task<Role> GetRoleById(int id);
        bool UpdateRole(Role role);
        bool Remove(Role role);
    }
}
