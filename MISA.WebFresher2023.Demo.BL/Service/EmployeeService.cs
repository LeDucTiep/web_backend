using AutoMapper;
using Dapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;
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
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper) : base(employeeRepository, mapper)
        {
        }

        public async Task<bool> CheckEmployeeCode(string code)
        {
            return await ((EmployeeRepository)_baseRepository).CheckEmployeeCode(code);
        }

        public async Task<EmployeePage> GetPage(int pageSize, int pageNumber, string? employeeFilter)
        {
            return await ((EmployeeRepository)_baseRepository).GetPage(pageSize, pageNumber, employeeFilter);
        }
        public async Task<string> GetNewEmployeeCode()
        {
            return await ((EmployeeRepository)_baseRepository).GetNewEmployeeCode();
        }
    }
}
