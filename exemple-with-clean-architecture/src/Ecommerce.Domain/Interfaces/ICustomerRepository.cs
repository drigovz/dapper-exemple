using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repository;

namespace Ecommerce.Domain.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer, int>
    { }
}
