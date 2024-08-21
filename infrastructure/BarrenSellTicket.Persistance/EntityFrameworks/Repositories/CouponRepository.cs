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
    public class CouponRepository : EfGenericRepository<Coupon>, ICouponRepository
    {
        private readonly BarrenSellTicketContext _context;
        public CouponRepository(BarrenSellTicketContext dbcontext) : base(dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<Coupon> GetByCodeAsync(string code)
        {
            var normalizedCode = code.ToUpper();
            return await _context.Coupons
                .Where(c => c.Code == normalizedCode)
                .FirstOrDefaultAsync();
              
        }
    }
}
