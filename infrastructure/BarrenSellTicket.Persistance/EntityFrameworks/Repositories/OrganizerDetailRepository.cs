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
        public OrganizerDetailRepository(BarrenSellTicketContext dbcontext) : base(dbcontext)
        {
        }

        //public async Task<OrganizerDetail> GetById(int id)
        //{
        //    var organizerDetail = await Table
        //        .Include(x => x.Address)
        //         .FirstOrDefaultAsync(x => x.Id == id);

        //    return organizerDetail;
        //}
    }
}
