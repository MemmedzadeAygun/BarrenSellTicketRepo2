using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Extensions
{
    public class SellTicketException:ApplicationException
    {
        public SellTicketException(string message)
            :base($"Sell ticket exception:{message}")
        {
            
        }
    }
}
