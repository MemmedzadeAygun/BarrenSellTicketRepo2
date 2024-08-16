using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks.Repositories
{
    public class BankAccountRepository : EfGenericRepository<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(BarrenSellTicketContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<BankAccount> GetById(int id)
        {
            return await Table
                 .Include(x => x.Customer)
                 .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
