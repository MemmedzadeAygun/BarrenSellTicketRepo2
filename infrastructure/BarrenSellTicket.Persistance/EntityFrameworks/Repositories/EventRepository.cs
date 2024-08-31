using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using BarrenSellTicket.Infrastructure.Helper;
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

        public async Task<List<Event>> GetEventsByOrganizerId(int createdId)
        {
            return await _context.Events
                .Include(x=>x.Image)
                .Where(x => x.CreatedId == createdId)
                .ToListAsync();
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

        public async Task<EventViewDto> GetEventViewById(int id)
        {
            return await _context.Events
                .Select(e => new EventViewDto
                {
                    Id = e.Id,
                    Name=e.Name,
                    EventDate = e.EventDate,
                    BeginTime = e.BeginTime,
                    EndTime = e.EndTime,
                    Duration = e.Duration,
                    Description = e.Description,
                    UserDetails = _context.Users
                    .Where(u => u.Id == e.CreatedId)
                    .Select(u => new CustomerDto
                    {
                        Id = u.Customer.Id,
                        FirstName = u.Customer.FirstName,
                        LastName = u.Customer.LastName,
                        Email = u.Customer.Email
                    })
                    .FirstOrDefault(),

                    Address = new AddressDto
                    {
                        City = e.Address.City,
                        Country = e.Address.Country,
                        Addres = e.Address.Addres
                    },

                    Category = new CategoryDto
                    {
                        Name = e.EventCategory.Name,
                    },

                    Type = new TypeDto
                    {
                        Name = e.EventType.Name
                    },

                    Image = e.Image.ImageUrl != null ? new ImageDto
                    {
                        ImageUrl = !string.IsNullOrEmpty(e.Image.ImageUrl) && File.Exists(e.Image.ImageUrl)
                        ? $"data:image/jpeg;base64,{ImageHelper.ImageToBase64(e.Image.ImageUrl)}"
                        : null,
                    } : null,

                    Tickets=e.Tickets.Select(ticket=>new TicketViewDto
                    {
                        Id=ticket.Id,
                        Price=ticket.Price,
                        AvailableCount=ticket.AvailableCount
                    }).ToList(),

                })
                .FirstOrDefaultAsync(e => e.Id == id);
                
        }
    }
}
