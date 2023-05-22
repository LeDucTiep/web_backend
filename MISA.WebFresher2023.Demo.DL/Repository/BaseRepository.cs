using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher2023.Demo.Common.Resource;
using MISA.WebFresher2023.Demo.DL.Entity;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MISA.WebFresher2023.Demo.DL.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly string _connectionString;
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"] ?? "";
        }
        /// <summary>
        /// Hàm xóa một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Mã lỗi</returns>
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var table = typeof(TEntity).Name;

            string procedure = $"Proc_{table}_Delete";

            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamicParams.Add($"{table.ToLower()}Id", id);

                await connection.QueryAsync(
                    procedure,
                    param: dynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                var result = dynamicParams.Get<int>("errorCode");

                return result;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public virtual async Task<TEntity?> GetAsync(Guid id)
        {
            var table = typeof(TEntity).Name;
            var connection = await GetOpenConnectionAsync();
            try
            {

                var sql = $"SELECT * FROM {table} Where {table}Id = @Id;";

                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("id", id);

                var entity = await connection.QueryFirstOrDefaultAsync<TEntity>(sql, dynamicParams);

                return entity;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public virtual async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        public virtual async Task<int?> PostAsync(TEntity entity)
        {
            var table = typeof(TEntity).Name;

            string procedure = $"Proc_{table}_Add";

            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Tạo id mới
                Guid newId = Guid.NewGuid();
                

                foreach (System.Reflection.PropertyInfo property in entity.GetType().GetProperties())
                {
                    var name = property.Name;

                    if(name == $"{table.ToLower()}Id")
                    {
                        property.SetValue(entity, newId, null);

                        dynamicParams.Add($"{table.ToLower()}Id", newId);
                    }

                    if (name == "modifiedBy" || name == "modifiedDate")
                        continue;

                    var value = property.GetValue(entity);

                    dynamicParams.Add(name, value);
                }

                dynamicParams.Add("CreatedBy", User.Name);
                dynamicParams.Add("CreatedDate", DateTime.Now);

                await connection.QueryAsync(
                    procedure,
                    param: dynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                var result = dynamicParams.Get<int>("errorCode");

                return result;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        public virtual async Task<int?> UpdateAsync(Guid id, TEntity entity)
        {
            var table = typeof(TEntity).Name;

            string procedure = $"Proc_{table}_Edit";

            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);

                
                foreach (System.Reflection.PropertyInfo property in entity.GetType().GetProperties())
                {
                    var name = property.Name;

                    if (name == $"{table.ToLower()}Id")
                    {
                        dynamicParams.Add($"{table.ToLower()}Id", id);
                    }

                    if (name == "createdBy" || name == "createdDate")
                        continue;

                    var value = property.GetValue(entity);
                    dynamicParams.Add(name, value);
                }

                dynamicParams.Add("modifiedBy", User.Name);
                dynamicParams.Add("modifiedDate", DateTime.Now);

                await connection.QueryAsync(
                    procedure,
                    param: dynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                var result = dynamicParams.Get<int>("errorCode");

                return result;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}
