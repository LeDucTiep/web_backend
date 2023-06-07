using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher2023.Demo.Common.MyException;
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
        #region Field
        /// <summary>
        /// Chuỗi kết nối 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        protected readonly string _connectionString;
        #endregion

        #region Contructor
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"] ?? "";
        }
        #endregion

        #region Method
        /// <summary>
        /// Hàm xóa một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Mã lỗi</returns>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            // Tên bảng 
            var table = typeof(TEntity).Name;

            // Tên procedure
            string procedure = ProcedureResource.Delete(table);

            // Connection với database 
            var connection = await GetOpenConnectionAsync();

            try
            {
                // Khởi tạo các tham số 
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                dynamicParams.Add($"{table}Id", id);

                await connection.ExecuteAsync(
                    procedure,
                    param: dynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                // Mã lỗi trả về 
                var result = dynamicParams.Get<int>("errorCode");

                return result;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Mã lỗi</returns>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task DeleteManyAsync(Guid[] arrayId)
        {
            // Tên bảng 
            var table = typeof(TEntity).Name;

            // Tên procedure
            string procedure = ProcedureResource.DeleteMany(table);

            // Connection với database 
            var connection = await GetOpenConnectionAsync();

            // Chuyển thành dạng danh sách
            List<Guid> listId = arrayId.ToList();

            // Còn thở thì còn xóa
            while (listId.Count > 0)
            {
                string param = "";
                int counterFlag = 0;

                // Xóa mỗi lần 10 bản ghi
                while (listId.Count > 0 && counterFlag < 10)
                {
                    Guid guid = listId[0];

                    param += $"'{guid}'";

                    listId.RemoveAt(0);

                    counterFlag++;

                    // Nếu là phần tử cuối cùng thì không cần dấu ,
                    if (listId.Count > 0 && counterFlag < 10)
                    {
                        param += ",";
                    }
                }

                try
                {
                    // Khởi tạo các tham số 
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add($"arrayId", param);

                    await connection.ExecuteAsync(
                        procedure,
                        param: dynamicParams,
                        commandType: CommandType.StoredProcedure
                    );
                }
                catch( Exception ex )
                {
                    throw new InternalException($"Procedure: {procedure}, Param: {param}, Message: {ex.Message}");
                }
                finally
                {
                    await connection.CloseAsync();
                }
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
            // Tên bảng
            var table = typeof(TEntity).Name;

            // Kết nối với database
            var connection = await GetOpenConnectionAsync();
            try
            {
                var sql = $"SELECT * FROM {table} Where {table}Id = @Id;";

                // Tham số 
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("id", id);

                // Bản ghi trả về 
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
            // Tạo kết nối
            var connection = new MySqlConnection(_connectionString);

            // Mở kết nối 
            await connection.OpenAsync();

            // Trả về kết nối
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
            // Tên bảng
            var table = typeof(TEntity).Name;

            // Procedure
            string procedure = ProcedureResource.Add(table);

            // Mở kết nối tới database
            var connection = await GetOpenConnectionAsync();
            try
            {
                // Các tham số
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Tạo id mới
                Guid newId = Guid.NewGuid();

                var properties = entity.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    // Tên thuộc tính
                    var name = property.Name;

                    // Gán id mới
                    if (name == $"{table}Id")
                    {
                        property.SetValue(entity, newId, null);

                        dynamicParams.Add($"{table}Id", newId);
                    }

                    // Bỏ qua ngày sửa và người sửa 
                    if (name == "ModifiedBy" || name == "ModifiedDate")
                        continue;

                    // Giá trị của tham số 
                    var value = property.GetValue(entity);

                    // Thêm tham số 
                    dynamicParams.Add(name, value);
                }

                // Thêm người tạo 
                dynamicParams.Add("CreatedBy", UserResource.Name);
                // Thêm ngày tạo 
                dynamicParams.Add("CreatedDate", DateTime.Now);


                using var myTransaction = await connection.BeginTransactionAsync();
                try
                {
                    //Gọi procedure
                    await connection.ExecuteAsync(
                        procedure,
                        param: dynamicParams,
                        commandType: CommandType.StoredProcedure,
                        transaction: myTransaction
                    );

                    myTransaction.Commit();
                }
                catch
                {
                    myTransaction.Rollback();
                    throw new InternalException();
                }

                // Lấy mã lỗi trả về 
                var erroCode = dynamicParams.Get<int>("errorCode");

                // return mã lỗi
                return erroCode;
            }
            finally
            {
                // Đóng kết nối 
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
            // Tên bảng 
            var table = typeof(TEntity).Name;

            // Tên procedure
            string procedure = ProcedureResource.Update(table);

            // Mở kết nối tới database
            var connection = await GetOpenConnectionAsync();
            try
            {
                // Khởi tạo tham số 
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Duyệt qua tất cả thuộc tính của entity
                System.Reflection.PropertyInfo[] properties = entity.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    // Tên tham số
                    var name = property.Name;

                    // Gán id truyền vào 
                    if (name == $"{table}Id")
                    {
                        dynamicParams.Add($"{table}Id", id);
                        continue;
                    }

                    // Bỏ qua người tạo và ngày tạo
                    if (name == "CreatedBy" || name == "CreatedDate")
                        continue;

                    // Giá trị của tham số 
                    var value = property.GetValue(entity);

                    // Thêm tham số
                    dynamicParams.Add(name, value);
                }

                // Thêm người sửa 
                dynamicParams.Add("ModifiedBy", UserResource.Name);

                // Thêm ngày sửa 
                dynamicParams.Add("ModifiedDate", DateTime.Now);

                using var myTransaction = await connection.BeginTransactionAsync();
                try
                {
                    // Gọi procedure
                    await connection.ExecuteAsync(
                        procedure,
                        param: dynamicParams,
                        commandType: CommandType.StoredProcedure,
                        transaction: myTransaction
                    );

                    myTransaction.Commit();
                }
                catch
                {
                    myTransaction.Rollback();
                    throw new InternalException();
                }

                // Lấy mã lỗi trả về 
                var erroCode = dynamicParams.Get<int>("errorCode");

                // return mã lỗi
                return erroCode;
            }
            finally
            {
                // Đóng connection
            }
        }

        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Author: LeDucTiep (27/05/2023)
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            // Tên bảng
            var table = typeof(TEntity).Name;

            // Tạo connection
            var connection = await GetOpenConnectionAsync();

            // Tên procedure
            string procedure = ProcedureResource.GetAll(table);

            // Gọi đến procedure
            return await connection.QueryAsync<TEntity>(
                procedure,
                commandType: CommandType.StoredProcedure
            );
        }
        #endregion
    }
}
