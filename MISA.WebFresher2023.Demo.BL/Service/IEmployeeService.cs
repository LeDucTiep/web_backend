using Dapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public interface IEmployeeService : IBaseService<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        /// <summary>
        /// Hàm kiểm tra mã nhân viên đã tồn tại
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên </param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task<bool> CheckEmployeeCode(string employeeCode);

        /// <summary>
        /// Hàm lấy danh sách nhân viên theo trang
        /// </summary>
        /// <param name="pageSize">Kích thước trang </param>
        /// <param name="pageNumber">Số thứ tự trang </param>
        /// <param name="employeeSearchTerm">Từ khóa tìm kiếm </param>
        /// <returns>Trang nhân viên</returns>
        /// Author: LeDucTiep (23/05/2023)
        public Task<EmployeePage> GetPageAsync(int pageSize, int pageNumber, string? employeeSearchTerm);

        /// <summary>
        /// Lấy mã nhân viên mới 
        /// </summary>
        /// <returns>Mã nhân viên mới </returns>
        /// Author: LeDucTiep (23/05/2023)
        public Task<string> GetNewEmployeeCode();
    }
}
