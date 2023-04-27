using API.Repositories.Interfaces;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repositories.Implements
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(DatabaseContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Event>> GetAllAsync(
            Expression<Func<Event, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(i => i.User)
                .ToListAsync();
        }

        public override async Task<Event?> GetAsync(
            Expression<Func<Event, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(i => i.User)
                .FirstOrDefaultAsync();
        }
    }
}
