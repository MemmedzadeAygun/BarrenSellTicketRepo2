using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks.Repositories
{
    public class ImageRepository : EfGenericRepository<Image>, IImageRepository
    {
        public ImageRepository(BarrenSellTicketContext dbcontext) : base(dbcontext)
        {
        }
    }
}
