using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Interfaces;

public interface ITicketRepository:IRepository<Ticket>
{
    Task<List<Ticket>> GetAll();
    Task<Ticket> GetById(int id);
}
