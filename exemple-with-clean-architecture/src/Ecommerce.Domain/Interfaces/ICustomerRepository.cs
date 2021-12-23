using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer, int>
    {
        public Task<IEnumerable<Customer>> ListCustomersAsync(string sql, Dictionary<string, object> parameters);
    }
}
