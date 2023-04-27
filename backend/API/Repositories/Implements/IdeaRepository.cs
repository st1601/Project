using API.Repositories.Interfaces;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Repositories.Implements
{
    public class IdeaRepository : BaseRepository<Idea>, IIdeaRepository
    {
        public IdeaRepository(DatabaseContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Idea>> GetAllAsync(
            Expression<Func<Idea, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(i => i.User)
                .Include(i => i.Event)
                .Include(i => i.Categories)
                .ToListAsync();
        }

        public override async Task<Idea?> GetAsync(
            Expression<Func<Idea, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(i => i.User)
                .Include(i => i.Event)
                .Include(i => i.Categories)
                .FirstOrDefaultAsync();
        }
    }
}
