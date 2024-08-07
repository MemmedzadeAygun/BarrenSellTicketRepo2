using BarrenSellTicket.Domain.Entities.Events;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Interfaces;

public interface IImageRepository:IRepository<Image>
{
}
