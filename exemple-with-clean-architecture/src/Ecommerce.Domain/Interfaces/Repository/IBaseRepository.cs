using Ecommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Interfaces.Repository
{
    public interface IBaseRepository<E, T> where E : BaseEntity
    {
        Task<IEnumerable<E>> GetAllAsync();
        Task<IEnumerable<E>> GetWithParamsAsync(params dynamic[] properties);
        Task<E> GetAsync(T id);
        Task<E> AddAsync(E entity);
        Task<E> UpdateAsync(E entity);
        Task RemoveAsync(T id);
        Task<int> SaveRangeAsync(IEnumerable<E> list);
    }
}
