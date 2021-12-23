using Dapper;
using Dapper.Contrib.Extensions;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Data.Repository
{
    public abstract class BaseRepository<E, T> : BuildQueries<E>, IBaseRepository<E, int> where E : BaseEntity
    {
        private new readonly string _tableName;
        private readonly string connection = ConnectionStringConfig.GetConnection;

        protected BaseRepository(string tableName)
            :base(tableName)
        {
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName), "Table name is required for BaseRepository!");
        }

        #region Connection
        private SqlConnection SqlConnection() =>
            new SqlConnection(connection);

        protected IDbConnection CreateConnection()
        {
            var _connection = SqlConnection();
            _connection.Open();
            return _connection;
        }
        #endregion

        #region CRUD
        public async Task<IEnumerable<E>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                //string query = $@"SELECT * FROM [dbo].[{_tableName}] GO";
                string query = BuildSelectDescribed();

                return (await connection.QueryAsync<E>(query)).ToList();
            }
        }

        public async Task<IEnumerable<E>> GetWithParamsAsync(params dynamic[] properties)
        {
            using (var connection = CreateConnection())
            {
                string query = BuildSelectWithParamsQuery(properties);

                return (await connection.QueryAsync<E>(query)).ToList();
            }
        }

        public async Task<E> GetAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                string query = $@"SELECT * FROM [dbo].[{_tableName}] WHERE Id=@Id";

                var result = await connection.QuerySingleOrDefaultAsync<E>(query, new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"[dbo].[{_tableName}] with id [{id}] could not be found.");

                return result;
            }
        }

        public async Task<E> AddAsync(E entity)
        {
            using (var connection = CreateConnection())
            {
                entity.CreatedAt = DateTime.UtcNow;

                var resultId = await connection.InsertAsync(entity);
                if (resultId > 0)
                {
                    return await GetAsync(resultId);
                }
                else
                    return null;
            }
        }

        public async Task<E> UpdateAsync(E entity)
        {
            using (var connection = CreateConnection())
            {
                entity.CreatedAt = entity.CreatedAt;
                entity.UpdatedAt = DateTime.UtcNow;

                var result = await connection.UpdateAsync(entity);
                if (result)
                    return await GetAsync(entity.Id);
                else
                    return null;
            }
        }

        public async Task RemoveAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync($"DELETE FROM [dbo].[{_tableName}] WHERE Id=@Id", new { Id = id });
            }
        }

        public async Task<int> SaveRangeAsync(IEnumerable<E> list)
        {
            var inserted = 0;
            var query = BuildInsertQuery();
            using (var connection = CreateConnection())
            {
                inserted += await connection.ExecuteAsync(query, list);
            }

            return inserted;
        }
        #endregion
    }
}
