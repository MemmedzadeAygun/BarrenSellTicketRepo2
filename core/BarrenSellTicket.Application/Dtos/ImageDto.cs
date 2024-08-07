using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Dtos
{
    public class ImageDto:IMapTo<Image>
    {
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
