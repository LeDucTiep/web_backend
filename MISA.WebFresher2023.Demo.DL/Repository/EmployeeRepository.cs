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

namespace MISA.WebFresher2023.Demo.DL.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>,  IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Task<bool> CheckEmployeeCode(string employeeCode)
        {
            throw new NotImplementedException();
        }


        public override async Task<Employee?> UpdateAsync(Guid employeeId, Employee employee)
        {
            var connection = await GetOpenConnectionAsync();


            bool isRequiredFieldErrored = employee.EmployeeCode == null || employee.EmployeeCode == "" ||
                                                employee.FullName == null || employee.FullName == "" ||
                                                employee.DepartmentId == null;
            if (isRequiredFieldErrored)
            {
                return null;
            }

            try
            {
                // Tạo connection
                using var mySqlConnection = new MySqlConnection(_connectionString);
                // Tạo các tham số 
                var parameters = new DynamicParameters();
                parameters.Add("errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("employeeId", employeeId);
                parameters.Add("employeeCode", employee.EmployeeCode);
                parameters.Add("fullName", employee.FullName);
                parameters.Add("gender", employee.Gender);
                parameters.Add("dateOfBirth", employee.DateOfBirth);
                parameters.Add("email", employee.Email);
                parameters.Add("address", employee.Address);
                parameters.Add("phoneNumber", employee.PhoneNumber);
                parameters.Add("identityNumber", employee.IdentityNumber);
                parameters.Add("identityDate", employee.IdentityDate);
                parameters.Add("identityPlace", employee.IdentityPlace);
                parameters.Add("positionId", employee.PositionId);
                parameters.Add("departmentId", employee.DepartmentId);
                parameters.Add("modifiedDate", DateTime.Now);
                parameters.Add("modifiedBy", employee.ModifiedBy);

                // Gọi đến procedure
                await mySqlConnection.QueryAsync(
                    "Proc_Employee_Edit",
                    param: parameters,
                    commandType: CommandType.StoredProcedure
                );

                // Lấy giá trị trả về 
                int errorCode = parameters.Get<int>("errorCode");

                await connection.CloseAsync();

                if (errorCode == 0)
                {
                    return null;
                }
                return employee;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
