using API.Repositories.Interfaces;
using Data;
using Data.Entities;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implements
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(DatabaseContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Comment>> GetAllAsync(
            Expression<Func<Comment, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(i => i.User)
                .Include(i => i.Idea)
                .ToListAsync();
        }

        public override async Task<Comment?> GetAsync(
            Expression<Func<Comment, bool>>? predicate = null)
        {
            var dbSet = predicate == null ? _dbSet : _dbSet.Where(predicate);

            return await dbSet
                .Include(i => i.User)
                .Include(i => i.Idea)
                .FirstOrDefaultAsync();
        }
    }
}
