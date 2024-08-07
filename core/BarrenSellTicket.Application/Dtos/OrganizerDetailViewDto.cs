﻿using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Accounts;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Dtos
{
    public class OrganizerDetailViewDto:IMapTo<OrganizerDetail>
    {
        public int Id { get; set; }
        public ImageDto Image { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Phone { get; set; }
        public AddressDto Address { get; set; }

    }
}
