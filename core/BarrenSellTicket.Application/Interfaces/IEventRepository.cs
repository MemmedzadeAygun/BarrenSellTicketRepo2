using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Interfaces;

public interface IEventRepository:IRepository<Event>
{
    Task<List<Event>> GetAll();
    Task<List<Event>> GetAllEventImage();
    Task<Event> GetById(int id);
    Task<Event> GetEventById(int id);
    Task<List<Event>> GetEventTypeId(int typeId);
    Task<List<Event>> GetEventByCategoryId(int categoryId);
}
