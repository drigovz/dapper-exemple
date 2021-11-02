//using Dapper;
//using DapperExemple.Domain.Models;
//using Microsoft.Extensions.Configuration;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DapperExemple.Data.Repository
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly IDbConnection _connection;
//        private readonly IConfiguration _configuration;

//        public UserRepository(IConfiguration configuration)
//        {
//            _configuration = configuration;
//            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
//        }

//        public async Task<List<User>> GetAllAsync()
//        {
//            string query = @"SELECT * FROM [dbo].[Users]
//                             GO";

//            return (await _connection.QueryAsync<User>(query)).ToList();
//        }
//    }
//}
