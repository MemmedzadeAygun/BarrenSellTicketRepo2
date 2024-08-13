using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Dtos
{
    public class BankAccountDto:IMapTo<BankAccount>
    {
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public long AccountNumber { get; set; }
        public string SwiftCode { get; set; }
        public string Iban { get; set; }
    }
}
