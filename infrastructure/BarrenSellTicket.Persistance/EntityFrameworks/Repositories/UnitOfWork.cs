using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Application.Interfaces.Register;
using BarrenSellTicket.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BarrenSellTicketContext _context;
        private readonly Dictionary<Type, Object> _repositories;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(BarrenSellTicketContext context, IHttpContextAccessor httpContextAccessor)
        {
            _repositories = new Dictionary<Type, Object>();
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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
        public IUserRoleRepository UserRoleRepository => SetReppository<IUserRoleRepository>();

        public async Task Commit()
        {

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            IEnumerable<EntityEntry<BaseEntity>> entities = _context
              .ChangeTracker
              .Entries<BaseEntity>()
              .ToList();

            foreach (var entry in entities)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedId = Convert.ToInt32(userId);

                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedId = Convert.ToInt32(userId);
                }
            }

            await _context.SaveChangesAsync();
            //await _context.Database.CommitTransactionAsync();
        }


        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            throw new NotImplementedException();
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
