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
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var table = typeof(TEntity).Name;

            string procedure = $"Proc_{table}_Delete";

            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamicParams.Add($"{table}Id", id);

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

        /// <summary>
        /// Hàm lấy một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Giá trị của bản ghi</returns>
        /// Author: LeDucTiep (23/05/2023)
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

        /// <summary>
        /// Hàm tạo kết nối đến cơ sở dữ liệu
        /// </summary>
        /// <returns>Kết nối đến database</returns>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        /// <summary>
        /// Hàm thêm một bản ghi
        /// </summary>
        /// <param name="entity">Giá trị của bản ghi</param>
        /// <returns>Mã lỗi</returns>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task<int> PostAsync(TEntity entity)
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

                var properties = entity.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    var name = property.Name;

                    if(name == $"{table}Id")
                    {
                        property.SetValue(entity, newId, null);

                        dynamicParams.Add($"{table}Id", newId);
                    }

                    if (name == "ModifiedBy" || name == "ModifiedDate")
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

        /// <summary>
        /// Hàm update một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entity">Giá trị của bản ghi</param>
        /// <returns>Mã lỗi</returns>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task<int> UpdateAsync(Guid id, TEntity entity)
        {
            var table = typeof(TEntity).Name;

            string procedure = $"Proc_{table}_Edit";

            var connection = await GetOpenConnectionAsync();
            try
            {
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var properties = entity.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    var name = property.Name;

                    if (name == $"{table}Id")
                    {
                        dynamicParams.Add($"{table}Id", id);
                        continue;
                    }

                    if (name == "CreatedBy" || name == "CreatedDate")
                        continue;

                    var value = property.GetValue(entity);
                    dynamicParams.Add(name, value);
                }

                dynamicParams.Add("ModifiedBy", User.Name);
                dynamicParams.Add("ModifiedDate", DateTime.Now);

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
