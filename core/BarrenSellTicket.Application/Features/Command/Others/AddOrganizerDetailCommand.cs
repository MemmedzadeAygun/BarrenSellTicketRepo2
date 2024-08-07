using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others
{
    public class AddOrganizerDetailCommand:IMapTo<OrganizerDetail>,IMapTo<Image>,IMapTo<Address>,IRequest
    {
        public string Name { get; set; }
        public string About { get; set; }
        public string Phone { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Addres { get; set; }
        public string Description { get; set; }
    }
}
