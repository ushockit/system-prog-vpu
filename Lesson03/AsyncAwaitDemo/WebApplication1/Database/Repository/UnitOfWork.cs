using Domain.Entity;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext db;

        IRepository<Guid, Person> _peopleRepository;
        IRepository<Guid, Person> IUnitOfWork.PeopleRepository 
            => _peopleRepository ?? (_peopleRepository = new PeopleRepository(db));

        public UnitOfWork(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task SaveChangesAsync(CancellationToken token = default) => await db.SaveChangesAsync(token).ConfigureAwait(false);
    }
}
