﻿using BarrenSellTicket.Domain.Entities.Accounts;
using BarrenSellTicket.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Domain.Entities.Events
{
    public class Customer:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public Users User { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}
