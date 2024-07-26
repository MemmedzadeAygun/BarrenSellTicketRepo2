using BarrenSellTicket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Events
{
    public class Payouts:BaseEntity
    {
        public DateOnly DatePaid { get; set; }
        public DateOnly Date { get; set; }
        public string TransactionID { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
