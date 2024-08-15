using BarrenSellTicket.Application.Interfaces.Register;
using BarrenSellTicket.Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks.Repositories.Register
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly BarrenSellTicketContext _dbContext;
        public UserRoleRepository(BarrenSellTicketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserRole(UserRole userRoles)
        {
            await _dbContext.UserRole.AddAsync(userRoles);
        }

        public async Task<UserRole> GetUserById(int id)
        {
            return await _dbContext.Set<UserRole>().FindAsync(id);
        }

        public async Task<List<UserRole>> GetUserRoles()
        {
          return await _dbContext.Set<UserRole>().ToListAsync();
        }

        public async Task<List<UserRole>> GetWhere(Expression<Func<UserRole, bool>> expression)
        {
            return await _dbContext.Set<UserRole>().Where(expression).ToListAsync();
        }

        public async void GetRoleByUserId(int roleId)
        {
            var roles = await _dbContext.Set<UserRole>()
                  .Include(x => x.Role)
                  .Where(x => x.RoleId == roleId)
                  .ToListAsync();

            roles.Select(x => x.Role.RoleName);

        }

        public bool Remove(UserRole userRole)
        {
            var removed=_dbContext.Set<UserRole>().Remove(userRole);
            return removed.State == EntityState.Deleted;
        }
    }
}
