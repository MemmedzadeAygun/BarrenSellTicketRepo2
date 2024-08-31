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
    public class AddEventCommand:IMapTo<Event>,IMapTo<Address>,IMapTo<Image>,IRequest
    {
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Addres { get; set; }
        public int EventTypeId { get; set; }
        public int EventCategoryId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Description { get; set; }
    }
}
