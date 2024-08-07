using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others.UpdateCommand
{
    public class UpdateOrganizerDetailCommand:IMapTo<OrganizerDetail>, IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Phone { get; set; }
        public AddressDto Address { get; set; }
    }
}
