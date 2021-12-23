using Dapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Data.Repository.Customers
{
    public class CustomerRepository : BaseRepository<Customer, int>, ICustomerRepository
    {
        public CustomerRepository()
            : base("Customers")
        { }

        public async Task<IEnumerable<Customer>> ListCustomersAsync(string sql, Dictionary<string, object> parameters)
        {
            using (var connection = CreateConnection())
            {
                return (await connection.QueryAsync<Customer>(sql, new DynamicParameters(parameters))).ToList();
            }
        }
    }
}
