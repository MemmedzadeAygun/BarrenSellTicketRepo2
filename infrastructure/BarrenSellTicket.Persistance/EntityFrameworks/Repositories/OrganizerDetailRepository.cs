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

        public async Task<OrganizerDetail> GetById(int id)
        {
            return await Table
                .Include(x => x.Address)
                .Include(x => x.ProfileImage)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<OrganizerDetail> GetByIdWithManualLoading(int id)
        {
            return await Table
                .Where(x => x.Id == id)
                .Select(x => new OrganizerDetail
                {
                    Id = x.Id,
                    Name = x.Name,
                    About = x.About,
                    Phone = x.Phone,
                    Address = x.Address!=null? new Address
                    {
                        Id=x.Address.Id,
                        Country=x.Address.Country,
                        City=x.Address.City,
                        Addres=x.Address.Addres
                    }:null,
                    ProfileImage = x.ProfileImage!=null? new Image 
                    {
                        Id=x.ProfileImage.Id,
                        ImageUrl=x.ProfileImage.ImageUrl
                    }:null
                })
                .FirstOrDefaultAsync();
        }
    }
}
