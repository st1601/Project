using API.Repositories.Interfaces;
using Data;
using Data.Entities;

namespace API.Repositories.Implements
{
    public class ThumbRespository : BaseRepository<Thumb>, IThumbRespository
    {
        public ThumbRespository(DatabaseContext context) : base(context)
        {
        }
    }
}
