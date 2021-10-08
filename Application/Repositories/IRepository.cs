using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Repositories
{

    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();

        TEntity Get<TId>(TId id)  where TId : struct;

        void Add(TEntity value);

        void Update<TId>(TId id, TEntity item) where TId : struct;

        Task<int> SaveChangesAsync(CancellationToken token);

        void Delete<TId>(TId id) where TId : struct;
    }
}