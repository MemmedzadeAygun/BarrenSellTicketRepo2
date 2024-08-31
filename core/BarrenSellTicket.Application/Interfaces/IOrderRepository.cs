using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Interfaces;

public interface IOrderRepository: IRepository<Order>
{
    Task<List<OrderDto>> GetOrdersWithUserDetailsAsync();
    Task<OrderDto> GetOrderById(int id);
    Task<List<OrderDto>> GetOrdersByCreatedId(int createdId);
}
