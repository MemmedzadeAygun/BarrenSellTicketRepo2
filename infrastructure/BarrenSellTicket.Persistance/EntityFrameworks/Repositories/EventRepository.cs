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
    public class EventRepository : EfGenericRepository<Event>, IEventRepository
    {
        private readonly BarrenSellTicketContext _context;
        public EventRepository(BarrenSellTicketContext dbcontext) : base(dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<Event>> GetAll()
        {
            return await _context.Events
                .Include(x=>x.EventCategory)
                .Include(x=>x.Address)
                .Include(x=>x.EventType)
                .ToListAsync();
        }

        public async Task<List<Event>> GetAllEventImage()
        {
            return await _context.Events
                .Include(x => x.Image)
                .ToListAsync();
        }

        public async Task<Event> GetById(int id)
        {
            return await _context.Events
                .Include(x => x.EventCategory)
                .Include(x => x.Address)
                .Include(x => x.EventType)
                .FirstOrDefaultAsync();
        }
    }
}
