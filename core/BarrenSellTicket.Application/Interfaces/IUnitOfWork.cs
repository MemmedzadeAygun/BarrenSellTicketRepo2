using BarrenSellTicket.Application.Interfaces.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
        void Rollback();
        TRepository SetReppository<TRepository>();
        TRepository GetRepository<TRepository>() where TRepository: class;
        IAddressRepository AddressRepository { get; }
        IBankAccountRepository BankAccountRepository { get; }
        ICouponRepository CouponRepository { get; }
        IEventCategoryRepository EventCategoryRepository { get; }
        IEventRepository EventRepository { get; }
        IEventTypeRepository EventTypeRepository { get; }
        IImageRepository ImageRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrganizerDetailRepository OrganizerDetailRepository { get; }
        IPayoutsRepository PayoutsRepository { get; }
        ITicketRepository TicketRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IContactListRepository ContactListRepository { get; }
    }
}
