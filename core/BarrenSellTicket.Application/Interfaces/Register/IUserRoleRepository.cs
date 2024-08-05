using BarrenSellTicket.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Interfaces.Register
{
    public interface IUserRoleRepository
    {
        Task AddUserRole(UserRole userRoles);
        Task<List<UserRole>> GetUserRoles();
        bool Remove(UserRole userRole);
    }
}
