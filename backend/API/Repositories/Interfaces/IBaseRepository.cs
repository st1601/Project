using Data.Entities;
using System.Linq.Expressions;

namespace API.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);

        Task<T?> GetAsync(Expression<Func<T, bool>>? predicate = null);

        T Create(T entity);

        T Update(T entity);

        void Delete(T entity);

        int SaveChanges();

        IDatabaseTransaction DatabaseTransaction();
    }
}
