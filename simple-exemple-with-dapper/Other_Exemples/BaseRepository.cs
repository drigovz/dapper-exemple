//using Dapper;
//using Microsoft.Extensions.Configuration;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DapperExemple.Data.Repository
//{
//    public class BaseRepository<T> : IBaseRepository<T>
//    {
//        private readonly IDbConnection _connection;
//        private readonly IConfiguration _configuration;

//        public BaseRepository(IConfiguration configuration)
//        {
//            _configuration = configuration;
//            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
//        }

//        public async Task<IEnumerable<T>> GetAllAsync(string query) =>
//            (await _connection.QueryAsync<T>(query)).ToList();
//    }
//}
