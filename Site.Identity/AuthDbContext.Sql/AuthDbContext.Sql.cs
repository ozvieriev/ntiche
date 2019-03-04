using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Identity
{
    public partial class AuthDbContext
    {
        internal async Task<T> ExecuteReaderAsync<T>(string commandText, SqlParameter[] sqlParams)
        {
            using (var cmd = Database.Connection.CreateCommand())
            {
                cmd.CommandText = sqlParams.CommandText(commandText);

                cmd.Parameters.AddRange(sqlParams);

                await Database.Connection.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();

                var entity = ((IObjectContextAdapter)this).ObjectContext.Translate<T>(reader).FirstOrDefault();

                Database.Connection.Close();

                return entity;
            }
        }
        internal async Task<List<T>> ExecuteReaderCollectionAsync<T>(string commandText, SqlParameter[] sqlParams)
        {
            using (var cmd = Database.Connection.CreateCommand())
            {
                cmd.CommandText = sqlParams.CommandText(commandText);

                cmd.Parameters.AddRange(sqlParams);

                await Database.Connection.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();

                var list = ((IObjectContextAdapter)this).ObjectContext.Translate<T>(reader).ToList();

                Database.Connection.Close();

                return list;
            }
        }
        internal async Task<int> ExecuteNonQueryAsync(string commandText, SqlParameter[] sqlParams)
        {
            using (var cmd = Database.Connection.CreateCommand())
            {
                cmd.CommandText = sqlParams.CommandText(commandText);

                cmd.Parameters.AddRange(sqlParams);

                await Database.Connection.OpenAsync();

                var reader = await cmd.ExecuteNonQueryAsync();

                Database.Connection.Close();

                return reader;
            }
        }
    }
    internal static class SqlHelper
    {
        internal static SqlParameter ToSql(this object value, string key)
        {
            return object.Equals(value, null) ? new SqlParameter(key, DBNull.Value) : new SqlParameter(key, value);
        }

        internal static string CommandText(this SqlParameter[] sqlParams, string commandText)
        {
            StringBuilder builder = new StringBuilder(commandText);

            if (!object.Equals(sqlParams, null) && sqlParams.Length > 0)
            {
                foreach (var item in sqlParams)
                    builder.AppendFormat(" @{0}=@{0},", item.ParameterName);

                builder.Length--;
            }

            return builder.ToString();
        }
    }
}
