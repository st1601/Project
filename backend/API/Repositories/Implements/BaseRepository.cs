using API.Repositories.Interfaces;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repositories.Implements
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _dbSet;

        private readonly DatabaseContext _context;

        public BaseRepository(DatabaseContext context)
        {
            _dbSet = context.Set<T>();
            _context = context;
        }

        public T Create(T entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public IDatabaseTransaction DatabaseTransaction()
        {
            return new DatabaseTransaction(_context);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet.ToListAsync();
        }

        public virtual Task<T?> GetAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null ? _dbSet.FirstOrDefaultAsync() : _dbSet.FirstOrDefaultAsync(predicate);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public T Update(T entity)
        {
            return _dbSet.Update(entity).Entity;
        }
    }
}
