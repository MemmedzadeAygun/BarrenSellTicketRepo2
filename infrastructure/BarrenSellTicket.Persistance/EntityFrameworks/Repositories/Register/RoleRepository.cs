using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Features.Command.Register;
using BarrenSellTicket.Application.Interfaces.Register;
using BarrenSellTicket.Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks.Repositories.Register
{
    public class RoleRepository : IRoleRepository
    {
        private readonly BarrenSellTicketContext _dbcontext;
    
        public RoleRepository(BarrenSellTicketContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task AddRole(Role role)
        {
            await _dbcontext.Role.AddAsync(role);
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _dbcontext.Set<Role>().FindAsync(id);
        }

        public async Task<List<Role>> GetRoles()
        {
            return await _dbcontext.Set<Role>().ToListAsync();
        }

        public bool Remove(Role role)
        {
            var removed = _dbcontext.Set<Role>().Remove(role);
            return removed.State == EntityState.Deleted;
        }

        public bool UpdateRole(Role role)
        {
            _dbcontext.Entry(role).State = EntityState.Modified;
            return true;
        }
    }
}
