using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        protected readonly DbSet<TEntity> Entities;

        public Repository(DbContext context)
        {
            Context = context;

            Entities = Context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return Entities.ToList();
        }

        public TEntity Get<TId>(TId id) where TId: struct
        {
            return Entities.Find(id);
        }

        public void Add(TEntity value)
        {
             Entities.Add(value);
        }

        public void Update<TId>(TId id, TEntity item) where TId : struct
        {
            Entities.Attach(item);

            EntityEntry<TEntity> entry = Context.Entry(item);

            if (entry != null) entry.State = EntityState.Modified;
        }

        public Task<int> SaveChangesAsync(CancellationToken token)
        {
            return Context.SaveChangesAsync(token);
        }

        public void Delete<TId>(TId id) where TId : struct
        {
            TEntity item = Get(id);

            if (item != null)
                Entities.Remove(item);
        }

    }
}