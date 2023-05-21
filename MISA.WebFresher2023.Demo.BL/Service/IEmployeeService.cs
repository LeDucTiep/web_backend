using Dapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public interface IEmployeeService : IBaseService<EmployeeDto, EmployeeUpdateDto>
    {
        Task<bool> CheckEmployeeCode(string employeeCode);
        public Task<EmployeePageDto> GetPage(int pageSize, int pageNumber, string? employeeFilter);
        public Task<string> GetNewEmployeeCode();
        public Task<int> PostAsync(Employee employee);
        
    }
}
