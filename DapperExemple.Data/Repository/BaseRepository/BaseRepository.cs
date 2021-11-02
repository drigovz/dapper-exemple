using Dapper;
using Dapper.Contrib.Extensions;
using DapperExemple.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DapperExemple.Data.Repository.BaseRepository
{
    public abstract class BaseRepository<E, T> : IBaseRepository<E, int> where E : BaseEntity
    {
        private readonly string _tableName;

        protected BaseRepository(string tableName)
        {
            _tableName = tableName;
        }

        string connection = @"Server=(localdb)\MSSQLLocalDB; Initial Catalog=DbDapper;Integrated Security=true;";

        #region Helpers
        private SqlConnection SqlConnection() =>
            new SqlConnection(connection);

        private IDbConnection CreateConnection()
        {
            var _connection = SqlConnection();
            _connection.Open();
            return _connection;
        }

        private IEnumerable<PropertyInfo> GetProperties =>
            typeof(E).GetProperties();

        private List<string> GetListOfProperties()
        {
            var entityProperties = new List<string>();

            foreach (PropertyInfo prop in GetProperties)
            {
                var notMapped = typeof(E).GetProperty(prop.Name).GetCustomAttributes(typeof(ComputedAttribute), false);
                if (notMapped.Length == 0)
                    entityProperties.Add(prop.Name);
            }

            return entityProperties;
        }

        private List<string> GetListOfPropertiesWithoutId()
        {
            var entityProperties = new List<string>();

            foreach (PropertyInfo prop in GetProperties)
            {
                var notMapped = typeof(E).GetProperty(prop.Name).GetCustomAttributes(typeof(ComputedAttribute), false);
                if (notMapped.Length == 0)
                    if (prop.Name != "Id")
                        entityProperties.Add(prop.Name);
            }

            return entityProperties;
        }

        private string BuildInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO [dbo].[{_tableName}] ");
            insertQuery.Append("(");

            var properties = GetListOfPropertiesWithoutId();
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            // remove the last comma (;) and init insertion of values
            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");

            return insertQuery.ToString();
        }

        private string BuildUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");

            var properties = GetListOfProperties();
            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }

        #endregion

        #region CRUD
        public async Task<IEnumerable<E>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                string query = $@"SELECT * FROM [dbo].[{_tableName}] 
                                GO";

                return (await connection.QueryAsync<E>(query)).ToList();
            }
        }

        public async Task<E> GetAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                string query = $@"SELECT * FROM [dbo].[{_tableName}] 
                                 WHERE Id=@Id";

                var result = await connection.QuerySingleOrDefaultAsync<E>(query, new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");

                return result;
            }
        }

        public async Task<E> AddAsync(E entity)
        {
            //var insertQuery = BuildInsertQuery();
            using (var connection = CreateConnection())
            {
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
            //var updateQuery = BuildUpdateQuery();
            using (var connection = CreateConnection())
            {
                var result = await connection.UpdateAsync(entity);
                if (result)
                    return await GetAsync(entity.Id);
                else
                    return null;
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id=@Id", new { Id = id });
            }
        }
        #endregion

        // return number of entities saved on database
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
    }
}