using BarrenSellTicket.Application.Interfaces.Register;
using BarrenSellTicket.Domain.Entities.Accounts;
using BarrenSellTicket.Domain.Entities.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks.Repositories.Register
{
    public class UserRepository : IUserRepository
    {
        private readonly BarrenSellTicketContext _dbcontext;

        public UserRepository(BarrenSellTicketContext dbContext)
        {
            _dbcontext= dbContext;
        }
        public async Task AddUser(Users user)
        {
            await _dbcontext.Set<Users>().AddAsync(user);
        }

        public async Task<Users?> GetUsers(string email, string password)
        {
            return await _dbcontext.Set<Users>()
                .Include(x=>x.Customer)
                .FirstOrDefaultAsync(x => x.Email == email && x.PasswordHash == password);
        }

        public void UpdateUser(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
