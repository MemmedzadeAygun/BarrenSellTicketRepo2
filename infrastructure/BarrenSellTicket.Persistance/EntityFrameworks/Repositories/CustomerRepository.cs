using BarrenSellTicket.Application.Dtos;
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
    public class CustomerRepository : EfGenericRepository<Customer>, ICustomerRepository
    {
        private readonly BarrenSellTicketContext _dbcontext;
        public CustomerRepository(BarrenSellTicketContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<AddressDto> GetAddressById(int customerId)
        {
            var address = await _dbcontext.Customer
                .Where(a => a.Id == customerId)
                .Select(a => new AddressDto
                {
                    Country = a.Address.Country,
                    City = a.Address.City,
                    Addres = a.Address.Addres
                })
                .FirstOrDefaultAsync();

            return address;
        }
    }
}
