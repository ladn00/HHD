using Dapper;
using Npgsql;
using System.Data.Common;

namespace HHD.DAL
{
    public class DbHelper
    {
        public static string ConnString = @"Server=localhost;Port=5432;User id=postgres;Password=178982az;Database=HHD";

        public static async Task<int> ExecuteScalarAsync(string sql, object model)
        {
            using (var connection = new NpgsqlConnection(ConnString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<int>(sql, model);
            }
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
        {
            using (var connection = new NpgsqlConnection(ConnString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<T>(sql, model);
            }
        }
    }
}
