using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Dtos
{
    public class BankAccountViewDto:IMapTo<BankAccount>,IMapTo<Customer>
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public long AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
