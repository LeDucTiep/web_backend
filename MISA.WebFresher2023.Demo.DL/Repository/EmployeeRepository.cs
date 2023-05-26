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
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Hàm kiểm tra mã EmployeeCode đã tồn tại chưa
        /// </summary>
        /// <param name="employeeCode">EmployeeCode</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<bool> CheckEmployeeCode(string employeeCode)
        {
            // Tạo connection
            var connection = await GetOpenConnectionAsync();

            // Tên procedure
            string procedure = ResourceProcedure.EmployeeCheckExistCode;

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
        /// Hàm tạo ra mã employeeCode
        /// </summary>
        /// <returns>Mã employeeCode mới</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<string> GetNewEmployeeCode()
        {
            // Tạo connection
            var connection = await GetOpenConnectionAsync();

            // Tên procedure
            string procedure = ResourceProcedure.EmployeeCheckExistCode;

            int i = 0;
            int numberLength = 4;

            try
            {

                while (true)
                {
                    i++;
                    if (i % 10 == 0)
                    {
                        numberLength++;
                    }
                    // Tạo mã nhân viên mới
                    int randomNumber = new Random().Next(1, numberLength * 10 - 1);
                    string newEmployeeCode = "NV-" + $"{randomNumber}".PadLeft(numberLength, '0');

                    // Tạo các tham số 
                    var parameters = new DynamicParameters();
                    parameters.Add("employeeCode", newEmployeeCode);

                    // Gọi đến procedure
                    var res = await connection.QueryAsync<bool>(
                        procedure,
                        param: parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    // Trả về kết quả
                    bool isExitsted = res.FirstOrDefault();

                    if (!isExitsted)
                    {
                        return newEmployeeCode;
                    }
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
        /// <param name="employeeFilter">Từ khóa cần tìm kiếm theo tên hoặc theo mã nhân viên</param>
        /// <returns>Trang nhân viên</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<EmployeePage> GetPage(int pageSize, int pageNumber, string? employeeFilter)
        {
            // Tạo connection
            var connection = await GetOpenConnectionAsync();
            try
            {
                // Tạo tham số đầu vào 
                // IN _offset: Số bản ghi bị bỏ qua
                // IN _limit: Số bản ghi được lấy
                // IN employeeFilter: Từ khóa tìm kiếm, theo employeeCode hoặc FullName
                // OUT TotalRecord: Tổng số bản ghi tìm thấy
                var parameters = new DynamicParameters();
                int offset = (pageNumber - 1) * pageSize;
                parameters.Add("_offset", offset);
                parameters.Add("_limit", pageSize);
                parameters.Add("employeeFilter", employeeFilter ?? "");
                parameters.Add("totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Gọi procedure 
                var res = await connection.QueryAsync<EmployeeOutPage>(
                    "Proc_Employee_PagingByFullNameOrEmployeeCode",
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
    }
}
