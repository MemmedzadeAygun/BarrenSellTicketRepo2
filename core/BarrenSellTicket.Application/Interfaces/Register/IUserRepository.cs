using BarrenSellTicket.Domain.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Interfaces.Register
{
    public interface IUserRepository
    {
        Task AddUser(Users user);
        void UpdateUser(Users user);
        Task<Users> GetUserById(int id);
        Task<Users?> GetUsers(string email);
    }
}
