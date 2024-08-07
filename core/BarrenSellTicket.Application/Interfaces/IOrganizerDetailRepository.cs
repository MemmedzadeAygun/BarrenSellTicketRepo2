using BarrenSellTicket.Domain.Entities.Accounts;
using BarrenSellTicket.Domain.Entities.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Interfaces;

public interface IOrganizerDetailRepository:IRepository<OrganizerDetail>
{
    Task<OrganizerDetail> GetById(int id);

    Task<OrganizerDetail> GetByIdWithManualLoading(int id);
}
