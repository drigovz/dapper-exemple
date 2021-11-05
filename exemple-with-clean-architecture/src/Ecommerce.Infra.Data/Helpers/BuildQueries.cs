using Dapper.Contrib.Extensions;
using Ecommerce.Domain.Entities;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ecommerce.Infra.Data.Helpers
{
    public class BuildQueries<E> where E : BaseEntity
    {
        protected readonly string _tableName;

        public BuildQueries(string tableName)
        {
            _tableName = tableName;
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

        protected string BuildInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO [dbo].[{_tableName}] ");
            insertQuery.Append("(");

            var properties = GetListOfPropertiesWithoutId();
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            insertQuery.Remove(insertQuery.Length - 1, 1)
                       .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery.Remove(insertQuery.Length - 1, 1)
                       .Append(")");

            return insertQuery.ToString();
        }

        protected string BuildUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE [dbo].[{_tableName}] SET ");

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

        protected string BuildSelectWithParamsQuery(params dynamic[] properties)
        {
            var selectQuery = new StringBuilder($"SELECT ");

            foreach (var property in properties)
            {
                selectQuery.Append($"[{property}], ");
            }

            selectQuery.Remove(selectQuery.Length - 2, 1);
            selectQuery.Append($"FROM [dbo].[{_tableName}] ");
            selectQuery.Append(" GO");

            return selectQuery.ToString();
        }

        protected string BuildSelectDescribed()
        {
            var selectQuery = new StringBuilder($"SELECT ");

            var properties = GetListOfProperties();
            properties.ForEach(property =>
            {
                selectQuery.Append($"[{property}], ");
            });

            selectQuery.Remove(selectQuery.Length - 2, 1);
            selectQuery.Append($"FROM [dbo].[{_tableName}] ");
            selectQuery.Append("GO");

            return selectQuery.ToString();
        }
    }
}
