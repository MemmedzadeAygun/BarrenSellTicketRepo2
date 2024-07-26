using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.EntityFrameworks.Repositories;

public class EfGenericRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly BarrenSellTicketContext _dbcontext;

    protected DbSet<T> Table=>_dbcontext.Set<T>();
    public EfGenericRepository(BarrenSellTicketContext dbcontext)
    {
        _dbcontext=dbcontext;
    }
    public async Task<bool> AddAsync(T entity)
    {
        var added = await Table.AddAsync(entity);
        return added.State == EntityState.Added;
    }

    public async Task<List<T>> GetAllAsync(bool tracking = true)
    {
        if (tracking is false)
        {
            return await Table.AsNoTracking().ToListAsync();
        }

        return await Table.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public async Task<List<T>> GetWhere(Expression<Func<T, bool>> expression)
    {
        return await Table.Where(expression).ToListAsync();
    }

    public bool Remove(T entity)
    {
        var removed = Table.Remove(entity);
        return removed.State == EntityState.Deleted;
    }

    public bool Update(T entity)
    {
        _dbcontext.Entry(entity).State = EntityState.Modified;
        return true;
    }
}
