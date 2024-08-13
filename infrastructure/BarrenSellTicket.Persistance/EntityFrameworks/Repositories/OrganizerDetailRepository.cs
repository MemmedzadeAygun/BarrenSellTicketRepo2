using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Accounts;
using BarrenSellTicket.Domain.Entities.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks.Repositories
{
    public class OrganizerDetailRepository : EfGenericRepository<OrganizerDetail>, IOrganizerDetailRepository
    {
        private readonly BarrenSellTicketContext _context;
        public OrganizerDetailRepository(BarrenSellTicketContext dbcontext) : base(dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<OrganizerDetail>> GetAll()
        {
            return await _context.OrganizerDetails
                .Include(z => z.Address)
                .Include(z => z.ProfileImage)
                .ToListAsync();
        }

        public async Task<OrganizerDetail> GetById(int id)
        {
            return await Table
                .Include(x => x.Address)
                .Include(x => x.ProfileImage)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

       
    }
}
