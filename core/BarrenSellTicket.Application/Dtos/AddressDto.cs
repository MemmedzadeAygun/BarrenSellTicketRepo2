using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;

namespace BarrenSellTicket.Application.Dtos
{
    public class AddressDto :  IMapTo<Address>
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Addres { get; set; }
    }
}
