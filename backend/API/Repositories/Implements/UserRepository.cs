using API.Repositories.Interfaces;
using Data;
using Data.Entities;

namespace API.Repositories.Implements
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
