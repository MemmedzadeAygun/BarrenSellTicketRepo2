using BarrenSellTicket.Application.Interfaces.Register;
using BarrenSellTicket.Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<List<UserRole>> GetUserRoles()
        {
          return await _dbContext.Set<UserRole>().ToListAsync();
        }

        public bool Remove(UserRole userRole)
        {
            var removed=_dbContext.Set<UserRole>().Remove(userRole);
            return removed.State == EntityState.Deleted;
        }
    }
}
