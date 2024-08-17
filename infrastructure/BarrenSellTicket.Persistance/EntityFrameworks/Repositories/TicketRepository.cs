using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks.Repositories
{
    public class TicketRepository : EfGenericRepository<Ticket>, ITicketRepository
    {
        private readonly BarrenSellTicketContext _context;
        public TicketRepository(BarrenSellTicketContext dbcontext) : base(dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<Ticket>> GetAll()
        {
            return await _context.Tickets
                .Include(ticket=>ticket.Event)
                .ThenInclude(events=>events.EventType)
                .Include(ticket=>ticket.Event)
                .ThenInclude(events=>events.EventCategory)
                .Include(ticket=>ticket.Event)
                .ThenInclude(events=>events.Address)
                .ToListAsync();
                
        }

        public async Task<Ticket> GetById(int id)
        {
            return await _context.Tickets
                .Include(ticket => ticket.Event)
                .ThenInclude(events => events.EventType)
                .Include(ticket => ticket.Event)
                .ThenInclude(events => events.EventCategory)
                .Include(ticket => ticket.Event)
                .ThenInclude(events => events.Address)
                .FirstOrDefaultAsync(ticket => ticket.Id == id);
        }
    }
}
