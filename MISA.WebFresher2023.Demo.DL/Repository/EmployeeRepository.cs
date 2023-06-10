using MISA.WebFresher2023.Demo.DL.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using MISA.WebFresher2023.Demo.Common.MyException;
using System.IO;
using System.Net;
using MISA.WebFresher2023.Demo.DL.Model;
using MISA.WebFresher2023.Demo.Common.Resource;

namespace MISA.WebFresher2023.Demo.DL.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Contructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Method
        /// <summary>
        /// Hàm kiểm tra mã EmployeeCode đã tồn tại chưa
        /// </summary>
        /// <param name="employeeCode">EmployeeCode</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<bool> CheckExistedEmployeeCode(string employeeCode)
        {
            // Tạo connection
            var connection = await GetOpenConnectionAsync();

            // Tên procedure
            string procedure = ProcedureResource.CheckDuplicatedCode;

            try
            {
                // Tạo các tham số 
                var parameters = new DynamicParameters();
                parameters.Add("employeeCode", employeeCode);

                // Gọi đến procedure
                bool result = await connection.QueryFirstAsync<bool>(
                    procedure,
                    param: parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        /// <summary>
        /// Hàm kiểm tra mã EmployeeCode muốn sửa đã tồn tại chưa
        /// </summary>
        /// <param name="employeeCode">EmployeeCode</param>
        /// <param name="itsCode">EmployeeCode trước khi sửa</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<bool> CheckDuplicatedEmployeeEditCode(string employeeCode, string itsCode)
        {
            // Tạo connection
            var connection = await GetOpenConnectionAsync();

            // Tên procedure
            string procedure = ProcedureResource.CheckDuplicatedCodeExceptItsCode;

            try
            {
                // Tạo các tham số 
                var parameters = new DynamicParameters();
                parameters.Add("employeeCode", employeeCode);
                parameters.Add("itsCode", itsCode);

                // Gọi đến procedure
                bool result = await connection.QueryFirstAsync<bool>(
                    procedure,
                    param: parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        /// <summary>
        /// Hàm kiểm tra mã EmployeeCode muốn sửa đã tồn tại chưa
        /// </summary>
        /// <param name="employeeCode">EmployeeCode</param>
        /// <param name="itsId">Id của bản ghi</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<bool> CheckDuplicatedEmployeeEditCode(string employeeCode, Guid itsId)
        {
            // Tạo connection
            var connection = await GetOpenConnectionAsync();

            // Tên procedure
            string procedure = ProcedureResource.CheckDuplicatedCodeExceptItsId;

            try
            {
                // Tạo các tham số 
                var parameters = new DynamicParameters();
                parameters.Add("employeeCode", employeeCode);
                parameters.Add("itsId", itsId);

                // Gọi đến procedure
                bool result = await connection.QueryFirstAsync<bool>(
                    procedure,
                    param: parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        
        /// <summary>
        /// Hàm chuyển chuỗi sang số 
        /// </summary>
        /// <param name="input">Chuỗi kí tự</param>
        /// <returns>Số int</returns>
        /// Author: LeDucTiep (27/05/2023)
        static long GetNumbers(string input)
        {
            try
            {
                return long.Parse(new string(input.Where(c => char.IsDigit(c)).ToArray()));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Hàm tạo ra mã employeeCode
        /// </summary>
        /// <returns>Mã employeeCode mới</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<string> GetNewEmployeeCode()
        {
            // Tạo connection
            var connection = await GetOpenConnectionAsync();

            // Tên procedure
            string procedure = ProcedureResource.CheckDuplicatedCode;

            int numberUp = 1;

            try
            {
                // Tạo mã mới
                string sql = "SELECT MAX(e.EmployeeCode) FROM employee e;";
                IEnumerable<string> enumerable = await connection.QueryAsync<string>(sql);
                string maxCode = enumerable.First();

                long code = GetNumbers(maxCode);

                while (true)
                {
                    code += numberUp;
                    string newEmployeeCode = $"NV-{code.ToString().PadLeft(4, '0')}";

                    // Kiểm tra mã mới có bị trùng không
                    // Tạo các tham số 
                    var parameters = new DynamicParameters();
                    parameters.Add("employeeCode", newEmployeeCode);

                    // Gọi đến procedure
                    bool isExitsted = await connection.QueryFirstAsync<bool>(
                        procedure,
                        param: parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    if (!isExitsted)
                    {
                        return newEmployeeCode;
                    }
                    numberUp++;
                }
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        /// <summary>
        /// Hàm lấy một trang thông tin nhân viên
        /// </summary>
        /// <param name="pageSize">Số lượng nhân viên trong một trang</param>
        /// <param name="pageNumber">Số thứ tự trang</param>
        /// <param name="employeeSearchTerm">Từ khóa cần tìm kiếm theo tên hoặc theo mã nhân viên</param>
        /// <returns>Trang nhân viên</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<EmployeePage> GetPageAsync(EmployeePageArgument employeePageArgument)
        {
            // Tạo connection
            var connection = await GetOpenConnectionAsync();

            string procedure = ProcedureResource.Paging;

            try
            {
                // Tạo tham số đầu vào 
                // IN _offset: Số bản ghi bị bỏ qua
                // IN _limit: Số bản ghi được lấy
                // IN employeeSearchTerm: Từ khóa tìm kiếm, theo employeeCode hoặc FullName
                // OUT TotalRecord: Tổng số bản ghi tìm thấy
                var parameters = new DynamicParameters();
                int offset = (employeePageArgument.PageNumber - 1) * employeePageArgument.PageSize;
                parameters.Add("_offset", offset);
                parameters.Add("_limit", employeePageArgument.PageSize);
                parameters.Add("employeeSearchTerm", employeePageArgument.EmployeeSearchTerm ?? "");
                parameters.Add("totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Gọi procedure 
                var res = await connection.QueryAsync<EmployeeInPage>(
                    procedure,
                    param: parameters,
                    commandType: CommandType.StoredProcedure
                );

                // Lấy tổng số trang 
                var totalRecord = parameters.Get<int>("totalRecord");

                // trả về kết quả
                return new EmployeePage(totalRecord, res);
            }
            finally
            {
                // Dong connection
                await connection.CloseAsync();
            }
        }

        /// <summary>
        /// Hàm lấy dữ liệu để xuất employee 
        /// </summary>
        /// <returns>EmployeeExport</returns>
        /// Author: LeDucTiep (07/06/2023)
        public async Task<IEnumerable<EmployeeExport>> GetEmployeeExportAsync()
        {
            // Tạo connection
            var connection = await GetOpenConnectionAsync();

            string procedure = ProcedureResource.EmployeeExport;

            try
            {
                // Gọi procedure 
                var res = await connection.QueryAsync<EmployeeExport>(
                    procedure,
                    commandType: CommandType.StoredProcedure
                );

                // trả về kết quả
                return res;
            }
            finally
            {
                // Dong connection
                await connection.CloseAsync();
            }
        }
        #endregion
    }
}
