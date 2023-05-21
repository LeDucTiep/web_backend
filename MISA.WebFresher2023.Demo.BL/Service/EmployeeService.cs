using AutoMapper;
using Dapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeUpdateDto>, IEmployeeService
    {
        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper) : base(employeeRepository, mapper)
        {
        }

        public override async Task<EmployeeDto?> UpdateAsync(Guid employeeId, EmployeeUpdateDto employeeUpdateDto)
        {
            // Validate employeeUpdateDto 


            var employee = await _baseRepository.GetAsync(employeeId);

            if (employee == null) { throw new Exception("Khong tim thay empployee"); }

            var newEmployee = _mapper.Map<Employee>(employeeUpdateDto);


            await _baseRepository.UpdateAsync(employeeId, newEmployee);

            return _mapper.Map<EmployeeDto>(employee);

        }

        public async Task<bool> CheckEmployeeCode(string code)
        {
            // Tạo connection
            var connection = await _baseRepository.GetOpenConnectionAsync();

            // Tạo các tham số 
            var parameters = new DynamicParameters();
            parameters.Add("employeeCode", code);

            // Gọi đến procedure
            bool result = await connection.QueryFirstAsync<bool>(
                "Proc_Employee_CheckExistCode",
                param: parameters,
                commandType: CommandType.StoredProcedure
            );

            await connection.CloseAsync();

            return result;
        }

        public async Task<EmployeePageDto> GetPage(int pageSize, int pageNumber, string? employeeFilter)
        {
            // Tạo connection
            var connection = await _baseRepository.GetOpenConnectionAsync();

            // Tạo tham số đầu vào 
            // IN _offset: Số bản ghi bị bỏ qua
            // IN _limit: Số bản ghi được lấy
            // IN employeeFilter: Từ khóa tìm kiếm, theo employeeCode hoặc FullName
            // OUT TotalRecord: Tổng số bản ghi tìm thấy
            var parameters = new DynamicParameters();
            int offset = pageNumber <= 0 ? 0 : (pageNumber - 1) * pageSize;
            parameters.Add("_offset", offset);
            parameters.Add("_limit", pageSize);
            parameters.Add("employeeFilter", employeeFilter ?? "");
            parameters.Add("totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Gọi procedure 
            var res = await connection.QueryAsync<Employee>(
                "Proc_Employee_PagingByFullNameOrEmployeeCode",
                param: parameters,
                commandType: CommandType.StoredProcedure
            );

            // Lấy tổng số trang 
            var totalRecord = parameters.Get<int>("totalRecord");

            // Dong connection
            await connection.CloseAsync();

            // trả về kết quả
            return new EmployeePageDto(totalRecord, res);
        }
        public async Task<string> GetNewEmployeeCode()
        {
            // Tạo connection
            var connection = await _baseRepository.GetOpenConnectionAsync();

        TopOfGetNewEmployeeCode:
            // Tạo mã nhân viên mới
            int numberLength = 6;
            int randomNumber = new Random().Next(1, numberLength * 10 - 1);
            string newEmployeeCode = "NV-" + $"{randomNumber}".PadLeft(numberLength, '0');


            // Tạo các tham số 
            var parameters = new DynamicParameters();
            parameters.Add("newCode", newEmployeeCode);

            // Gọi đến procedure
            var res = await connection.QueryAsync<bool>(
                "Proc_Employee_CheckNewCode",
                param: parameters,
                commandType: CommandType.StoredProcedure
            );

            // Trả về kết quả
            bool x = res.FirstOrDefault();

            if (x)
            {
                await connection.CloseAsync();
                return newEmployeeCode;
            }
            else
                goto TopOfGetNewEmployeeCode;
        }
        public async Task<int> PostAsync(Employee employee)
        {
            try
            {
                // validate du lieu

                Guid employeeId = Guid.NewGuid();
                bool isRequiredFieldErrored = employee.EmployeeCode == null || employee.EmployeeCode == "" ||
                                                employee.FullName == null || employee.FullName == "" ||
                                                employee.DepartmentId == null;
                if (isRequiredFieldErrored)
                {
                    return 1;
                }
                // Tạo connection
                var connection = await _baseRepository.GetOpenConnectionAsync();

                // Tạo các tham số 
                var parameters = new DynamicParameters();
                parameters.Add("errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("employeeId", employeeId);
                parameters.Add("employeeCode", employee.EmployeeCode);
                parameters.Add("fullName", employee.FullName);
                parameters.Add("gender", value: employee.Gender);
                parameters.Add("dateOfBirth", value: employee.DateOfBirth);
                parameters.Add("email", value: employee.Email ?? "");
                parameters.Add("address", value: employee.Address ?? "");
                parameters.Add("phoneNumber", value: employee.PhoneNumber ?? "");
                parameters.Add("identityNumber", value: employee.IdentityNumber ?? "");
                parameters.Add("identityDate", value: employee.IdentityDate);
                parameters.Add("identityPlace", value: employee.IdentityPlace ?? "");
                parameters.Add("positionId", value: employee.PositionId);
                parameters.Add("departmentId", employee.DepartmentId);
                parameters.Add("createdDate", DateTime.Now);
                parameters.Add("createdBy", value: employee.CreatedBy ?? "");

                // Gọi đến procedure
                await connection.QueryAsync(
                    "Proc_Employee_Add",
                    param: parameters,
                    commandType: CommandType.StoredProcedure
                );

                // Lấy giá trị trả về 
                int resCode = parameters.Get<int>("errorCode");
                return resCode;
            }
            catch (Exception)
            {
                //return StatusCode(400);
                return 1;
            }
        }

        public override async Task<int> DeleteAsync(Guid id)
        {
            // Tạo connection
            var connection = await _baseRepository.GetOpenConnectionAsync();

            // Tạo các tham số 
            var parameters = new DynamicParameters();
            parameters.Add("errorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("employeeId", id);

            // Gọi đến procedure
            await connection.QueryAsync(
                "Proc_Employee_Delete",
                param: parameters,
                commandType: CommandType.StoredProcedure
            );

            // Lấy giá trị trả về 
            int errorCode = parameters.Get<int>("errorCode");

            await connection.CloseAsync();

            if (errorCode == 0)
            {
                return 0;
            }
            return 1;
        }
    }
}
