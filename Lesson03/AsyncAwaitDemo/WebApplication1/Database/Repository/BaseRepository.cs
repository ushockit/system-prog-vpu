using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public abstract class BaseRepository<TKey, TValue> : IRepository<TKey, TValue>
        where TKey : struct
        where TValue : BaseEntity<TKey>
    {
        protected readonly ApplicationDbContext db;

        protected DbSet<TValue> Table => db.Set<TValue>();

        public BaseRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task CreateAsync(TValue entity, CancellationToken token = default)
        {
            db.Entry(entity).State = EntityState.Added;
        }

        public abstract Task<TValue> GetAsync(TKey id, CancellationToken token = default);

        public async Task<IEnumerable<TValue>> GetAllAsync(CancellationToken token = default) => await Table.ToListAsync(token).ConfigureAwait(false);

        public abstract Task RemoveAsync(TKey id, CancellationToken token = default);

        public abstract Task UpdateAsync(TValue entity, CancellationToken token = default);

        public async Task<IEnumerable<TValue>> GetAllAsync(Func<TValue, bool> predicate, CancellationToken token = default)
        {
            return await Task.Run(() => Table.Where(predicate).ToList(), token);
        }
    }
}
