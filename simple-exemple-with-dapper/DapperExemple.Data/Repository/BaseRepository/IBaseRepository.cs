using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperExemple.Data.Repository.BaseRepository
{
    public interface IBaseRepository<E, T>
    {
        Task<IEnumerable<E>> GetAllAsync();
        Task<E> GetAsync(T id);
        Task<E> AddAsync(E entity);
        Task<E> UpdateAsync(E entity);
        Task DeleteAsync(T id);
        Task<int> SaveRangeAsync(IEnumerable<E> list);
    }
}
