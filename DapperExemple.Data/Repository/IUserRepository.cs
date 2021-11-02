using DapperExemple.Data.Repository.BaseRepository;
using DapperExemple.Domain.Models;

namespace DapperExemple.Data.Repository
{
    public interface IUserRepository : IBaseRepository<User, int>
    { }
}
