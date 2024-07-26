using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Application.Interfaces.Register;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BarrenSellTicketContext _context;
        private readonly Dictionary<Type, Object> _repositories;

        public UnitOfWork(BarrenSellTicketContext context)
        {
            _repositories = new Dictionary<Type, Object>();
            _context = context;
        }

        public IAddressRepository AddressRepository => SetReppository<IAddressRepository>();
        public IBankAccountRepository BankAccountRepository => SetReppository<IBankAccountRepository>();
        public ICouponRepository CouponRepository => SetReppository<ICouponRepository>();
        public IEventCategoryRepository EventCategoryRepository => SetReppository<IEventCategoryRepository>();
        public IEventRepository EventRepository => SetReppository<IEventRepository>();
        public IEventTypeRepository EventTypeRepository => SetReppository<IEventTypeRepository>();
        public IImageRepository ImageRepository => SetReppository<IImageRepository>();
        public IOrderRepository OrderRepository => SetReppository<IOrderRepository>();
        public IOrganizerDetailRepository OrganizerDetailRepository => SetReppository<IOrganizerDetailRepository>();
        public IPayoutsRepository PayoutsRepository => SetReppository<IPayoutsRepository>();
        public ITicketRepository TicketRepository => SetReppository<ITicketRepository>();
        public IUserRepository UserRepository => SetReppository<IUserRepository>();
        public IRoleRepository RoleRepository => SetReppository<IRoleRepository>();
        public ICustomerRepository CustomerRepository => SetReppository<ICustomerRepository>();
        public IContactListRepository ContactListRepository => SetReppository<IContactListRepository>();

        public async Task Commit()
        {
           await _context.SaveChangesAsync();
        }


        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            return SetReppository<TRepository>();
        }

        public void Rollback()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State= EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State= EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        public TRepository SetReppository<TRepository>()
        {
            if (_repositories.ContainsKey(typeof(TRepository)))
            {
                return (TRepository)_repositories[typeof(TRepository)];
            }

            var repositoryType = Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(x=>!x.IsInterface && x.IsClass && x.IsAssignableTo(typeof(TRepository)));

            var repositoryInstance = (TRepository)Activator.CreateInstance(repositoryType,_context);
            _repositories.Add(typeof(TRepository),repositoryInstance);
            return repositoryInstance;
        }
    }
}
