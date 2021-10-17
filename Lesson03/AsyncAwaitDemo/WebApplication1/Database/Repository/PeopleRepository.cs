using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class PeopleRepository : BaseRepository<Guid, Person>
    {
        public PeopleRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public override async Task<Person> GetAsync(Guid id, CancellationToken token = default)
            => await Table.FirstOrDefaultAsync(person => person.Id == id, token).ConfigureAwait(false);

        public override async Task RemoveAsync(Guid id, CancellationToken token = default)
        {
            var person = await GetAsync(id, token);
            db.Entry(person).State = EntityState.Deleted;
        }

        public override async Task UpdateAsync(Person entity, CancellationToken token = default)
        {
            var person = await GetAsync(entity.Id, token);

            person.LastName = entity.LastName;
            person.FirstName = entity.FirstName;
            person.Birth = entity.Birth;

            db.Entry(person).State = EntityState.Modified;
        }
    }
}
