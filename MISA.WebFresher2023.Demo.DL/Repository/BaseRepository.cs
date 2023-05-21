using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher2023.Demo.DL.Entity;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly string _connectionString;
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"] ?? "";
        }
        public virtual Task<TEntity?> DeleteAsync(Guid id, TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var table = typeof(TEntity).Name;

            var connection = await GetOpenConnectionAsync();

            var sqlDelete = $"DELETE FROM {table} Where {table}Id = @Id;";

            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("id", id);

            var result = await connection.ExecuteAsync(sqlDelete, dynamicParams);

            await connection.CloseAsync();

            return result;
        }

        public virtual async Task<TEntity?> GetAsync(Guid id)
        {
            var table = typeof(TEntity).Name;

            var connection = await GetOpenConnectionAsync();

            var sql = $"SELECT * FROM {table} Where {table}Id = @Id;";

            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("id", id);

            var entity = await connection.QueryFirstOrDefaultAsync<TEntity>(sql, dynamicParams);

            await connection.CloseAsync();

            return entity;
        }

        public virtual async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        public virtual Task<TEntity?> UpdateAsync(Guid id, TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
