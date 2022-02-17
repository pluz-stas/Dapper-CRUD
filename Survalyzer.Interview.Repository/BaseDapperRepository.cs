using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Survalyzer.Interview.Repository
{
    public class BaseDapperRepository
    {
        private readonly string _connectionString;
    
        public BaseDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        protected async Task<TResult> ExecuteAsync<TResult>(Func<IDbConnection, Task<TResult>> func)
        {
            using var db = GetConnection();
            var result = await func(db);
            return result;
        }

        protected async Task<TResult> ExecuteScalarAsync<TResult>(Func<IDbConnection, Task<TResult>> func)
            where TResult : struct
        {
            using var db = GetConnection();
            var result = await func(db);
            return result;
        }
    }
}