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

        public IQueryable<Event> GetAll()
        {
            return  _context.Events
                .Include(x => x.EventCategory)
                .Include(x => x.Address)
                .Include(x => x.EventType)
                .Include(x => x.Tickets)
                .Include(x => x.Image)
                .AsQueryable();
                //.ToListAsync();
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
                .Include(x => x.EventType)
                .Include(x => x.Address) 
                .Include(x=>x.Tickets)
                .Include(x=>x.Image)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<List<Event>> GetEventByCategoryId(int categoryId)
        {
            return await _context.Events
                .Include(e => e.EventType)
                .Include(e => e.Address)
                .Include(e => e.Tickets)
                .Include(e=>e.Image)
                .Include(e => e.EventCategory)
                .Where(e => e.EventCategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Event> GetEventById(int id)
        {
            return await _context.Events           
                .Include(x => x.Address)          
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Event>> GetEventTypeId(int typeId)
        {
            return await _context.Events
                 .Include(x => x.EventCategory)
                 .Include(x => x.Address)
                 .Include(x=>x.EventType)
                 .Where(x => x.EventTypeId == typeId)
                 .ToListAsync();
        }

    }
}
