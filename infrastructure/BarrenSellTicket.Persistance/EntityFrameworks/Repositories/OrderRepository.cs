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
    public class OrderRepository : EfGenericRepository<Order>, IOrderRepository
    {
        private readonly BarrenSellTicketContext _context;
        public OrderRepository(BarrenSellTicketContext dbcontext) : base(dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            return await _context.Orders
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    Quantity = o.Quantity,
                    UserDetails = _context.Users
                    .Where(u => u.Id == o.CreatedId)
                    .Select(u => new CustomerDto
                    {
                        Id = u.Customer.Id,
                        FirstName = u.Customer.FirstName,
                        LastName = u.Customer.LastName,
                        Email = u.Customer.Email,
                    })
                    .FirstOrDefault(),

                    Ticket=_context.Tickets
                    .Where(t=>t.Id==o.TicketId)
                    .Select(t=>new TicketDto
                    {
                        Id = t.Id,
                        Price=t.Price,
                        Event=new EventViewDto
                        {
                            Id=t.Event.Id,
                            Name=t.Event.Name,
                            EventDate=t.Event.EventDate,
                            Duration=t.Event.Duration,
                            Description=t.Event.Description,

                            Address=new AddressDto
                            {
                                City=t.Event.Address.City,
                                Country=t.Event.Address.Country,
                                Addres=t.Event.Address.Addres
                            },

                            Category=new CategoryDto
                            {
                                Name=t.Event.EventCategory.Name
                            },

                            Image=t.Event.Image!=null? new ImageDto
                            {
                                ImageUrl=!string.IsNullOrEmpty(t.Event.Image.ImageUrl) && File.Exists(t.Event.Image.ImageUrl)
                                ? $"data:image/jpeg;base64,{ImageHelper.ImageToBase64(t.Event.Image.ImageUrl)}"
                                : null,
                            }:null,
                            Type=new TypeDto
                            {
                                Name=t.Event.EventType.Name
                            }
                        }
                    })
                    .FirstOrDefault()
                })
                .FirstOrDefaultAsync(o => o.Id == id);

        }

        public async Task<List<OrderDto>> GetOrdersByCreatedId(int createdId)
        {
            return await _context.Orders
             .Where(o => o.CreatedId == createdId)
             .Select(o => new OrderDto
             {
                 Id = o.Id,
                 OrderDate = o.OrderDate,
                 TotalAmount = o.TotalAmount,
                 Status = o.Status,
                 Quantity = o.Quantity,
                 UserDetails = _context.Users
                     .Where(u => u.Id == o.CreatedId)
                     .Select(u => new CustomerDto
                     {
                         Id = u.Customer.Id,
                         FirstName = u.Customer.FirstName,
                         LastName = u.Customer.LastName,
                         Email = u.Customer.Email,
                     })
                     .FirstOrDefault(),

                 Ticket = _context.Tickets
                     .Where(t => t.Id == o.TicketId)
                     .Select(t => new TicketDto
                     {
                         Id = t.Id,
                         Price = t.Price,
                         Event = new EventViewDto
                         {
                             Id = t.Event.Id,
                             Name = t.Event.Name,
                             EventDate = t.Event.EventDate,
                             Duration = t.Event.Duration,
                             Description = t.Event.Description,

                             Address = new AddressDto
                             {
                                 City = t.Event.Address.City,
                                 Country = t.Event.Address.Country,
                                 Addres = t.Event.Address.Addres
                             },

                             Category = new CategoryDto
                             {
                                 Name = t.Event.EventCategory.Name
                             },

                             Image = t.Event.Image != null ? new ImageDto
                             {
                                 ImageUrl = !string.IsNullOrEmpty(t.Event.Image.ImageUrl) && File.Exists(t.Event.Image.ImageUrl)
                                     ? $"data:image/jpeg;base64,{ImageHelper.ImageToBase64(t.Event.Image.ImageUrl)}"
                                     : null,
                             } : null,
                             Type = new TypeDto
                             {
                                 Name = t.Event.EventType.Name
                             }
                         }
                     })
                     .FirstOrDefault()
             })
             .ToListAsync();
        }

        public async Task<List<OrderDto>> GetOrdersWithUserDetailsAsync()
        {
            var orders = await _context.Orders
                 .Select(o => new OrderDto
                 {
                     Id = o.Id,
                     OrderDate = o.OrderDate,
                     TotalAmount = o.TotalAmount,
                     Status = o.Status,
                     Quantity = o.Quantity,
                     UserDetails = _context.Users
                     .Where(u => u.Id == o.CreatedId)
                     .Select(u => new CustomerDto
                     {
                         Id=u.Customer.Id,
                         FirstName = u.Customer.FirstName,
                         LastName = u.Customer.LastName,
                         Email=u.Customer.Email,
                     })
                     .FirstOrDefault(),

                     Ticket = _context.Tickets
                    .Where(t => t.Id == o.TicketId)
                    .Select(t => new TicketDto
                    {
                        Id = t.Id,
                        Price = t.Price,
                        Event = new EventViewDto
                        {
                            Id = t.Event.Id,
                            Name = t.Event.Name,
                            EventDate = t.Event.EventDate,
                            Duration = t.Event.Duration,
                            Description=t.Event.Description,

                            Address=new AddressDto
                            {
                                City=t.Event.Address.City,
                                Country=t.Event.Address.Country,
                                Addres=t.Event.Address.Addres
                            },

                            Category=new CategoryDto
                            {
                                Name=t.Event.EventCategory.Name
                            },

                            Image = t.Event.Image != null ? new ImageDto
                            {
                                ImageUrl = !string.IsNullOrEmpty(t.Event.Image.ImageUrl) && File.Exists(t.Event.Image.ImageUrl)
                                ? $"data:image/jpeg;base64,{ImageHelper.ImageToBase64(t.Event.Image.ImageUrl)}"
                                : null,
                            } : null,
                            Type = new TypeDto
                            {
                                Name = t.Event.EventType.Name
                            }
                        }
                    })
                    .FirstOrDefault()
                 })
                 .ToListAsync();

            return orders;
        }
    }
}
