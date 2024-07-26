using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks.Repositories
{
    public class CouponRepository : EfGenericRepository<Coupon>, ICouponRepository
    {
        public CouponRepository(BarrenSellTicketContext dbcontext) : base(dbcontext)
        {
        }
    }
}
