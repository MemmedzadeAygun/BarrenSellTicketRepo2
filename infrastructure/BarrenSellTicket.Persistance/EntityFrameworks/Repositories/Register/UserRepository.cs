using BarrenSellTicket.Application.Dtos;
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

        public async Task<Users> GetUserById(int id)
        {
           return await _dbcontext.Set<Users>().FindAsync(id);
        }

        public async Task<CustomerDto> GetUserDetailsById(int userId)
        {
            var userDetail = await _dbcontext.Customer
                .Where(c => c.UserId == userId)
                .Select(c => new CustomerDto
                {
                    Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                })
                .FirstAsync();

            return userDetail;

        }

        public async Task<Users?> GetUsers(string email)
        {
            return await _dbcontext.Set<Users>()
                .Include(x=>x.Customer)
                .FirstOrDefaultAsync(x => x.Email == email);
        }


        public void UpdateUser(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
