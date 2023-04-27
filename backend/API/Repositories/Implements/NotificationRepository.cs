using API.Repositories.Interfaces;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repositories.Implements
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(DatabaseContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Notification>> GetAllAsync(
            Expression<Func<Notification, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(i => i.Idea)
                    .ThenInclude(u => u.User)
                .Include(i => i.Idea)
                    .ThenInclude(e => e.Event)
                .ToListAsync();
        }

        public override async Task<Notification?> GetAsync(
            Expression<Func<Notification, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(i => i.Idea)
                    .ThenInclude(u => u.User)
                .Include(i => i.Idea)
                    .ThenInclude(e => e.Event)
                .FirstOrDefaultAsync();
        }
    }
}
