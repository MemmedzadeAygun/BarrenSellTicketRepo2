using BarrenSellTicket.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.Interceptors
{
    public class UpdateBaseEntityInterceptors: SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData 
            eventData, int result, CancellationToken cancellationToken = default)
        {

            var dbContext = eventData.Context;

            if(dbContext is null)
            {
                return base.SavedChangesAsync(eventData, result, cancellationToken);
            }

            IEnumerable<EntityEntry<BaseEntity>> entries = dbContext
                .ChangeTracker
                .Entries<BaseEntity>();

            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.Property(k => k.CreatedDate).CurrentValue =
                            DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entityEntry.Property(k => k.UpdatedDate).CurrentValue =
                            DateTime.UtcNow; 
                        break;
                }
            }

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
