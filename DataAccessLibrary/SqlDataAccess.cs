namespace DataAccessLibrary
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;
    using System.Linq;

    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        public string ConnectionStringName { get; set; } = "Default";
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }
        public async Task<List<T>> LoadData<T, U>(string sql, U parameters) // squigly stays long
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }
        public async Task SaveData<T>(string sql, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }

    }
}
