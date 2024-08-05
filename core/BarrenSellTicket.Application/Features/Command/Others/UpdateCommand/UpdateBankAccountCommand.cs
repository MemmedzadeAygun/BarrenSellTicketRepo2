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
    public class UpdateBankAccountCommand:IMapTo<BankAccount>,IRequest
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public long AccountNumber { get; set; }
        public string SwiftCode { get; set; }
        public string Iban { get; set; }
    }
}
