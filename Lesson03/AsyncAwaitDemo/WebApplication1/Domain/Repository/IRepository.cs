using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IRepository<TKey, TValue>
        where TKey : struct
        where TValue : BaseEntity<TKey>
    {
        Task<IEnumerable<TValue>> GetAllAsync(CancellationToken token = default);
        Task<IEnumerable<TValue>> GetAllAsync(Func<TValue, bool> predicate, CancellationToken token = default);
        Task<TValue> GetAsync(TKey id, CancellationToken token = default);
        Task CreateAsync(TValue entity, CancellationToken token = default);
        Task RemoveAsync(TKey id, CancellationToken token = default);
        Task UpdateAsync(TValue entity, CancellationToken token = default);
    } 
}
