﻿using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Interfaces
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        Task<AddressDto> GetAddressById(int customerId);
    }
}
