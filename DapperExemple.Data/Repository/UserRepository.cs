using DapperExemple.Data.Repository.BaseRepository;
using DapperExemple.Domain.Models;

namespace DapperExemple.Data.Repository
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        // pass table name for base class (BaseRepository) ==> : base("User")
        public UserRepository() : base("Users")
        { }
    }
}
