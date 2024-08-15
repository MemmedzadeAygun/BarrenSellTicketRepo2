using BarrenSellTicket.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Interfaces.Register
{
    public interface IUserRoleRepository
    {
        Task AddUserRole(UserRole userRoles);
        Task<List<UserRole>> GetUserRoles();
        bool Remove(UserRole userRole);
        Task<UserRole> GetUserById(int id);
        Task<List<UserRole>> GetWhere(Expression<Func<UserRole, bool>> expression);
        Task<List<string>> GetRoleNameByUserId(int id);
    }
}
