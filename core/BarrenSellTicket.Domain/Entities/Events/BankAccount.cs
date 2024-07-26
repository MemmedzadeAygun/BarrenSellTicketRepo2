using BarrenSellTicket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Events
{
    public class BankAccount:BaseEntity
    {
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public long AccountNumber { get; set; }
        public string SwiftCode { get; set; }
        public string Iban { get; set; }
        public ICollection<Payouts> Payouts { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
