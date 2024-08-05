using BarrenSellTicket.Application.Mappers.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others
{
    public class AddBankAccountCommand:IMapTo<BankAccount>,IRequest
    {
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public long AccountNumber { get; set; }
        public string SwiftCode { get; set; }
        public string Iban { get; set; }
        public int? CustomerId { get; set; }
    }
}
