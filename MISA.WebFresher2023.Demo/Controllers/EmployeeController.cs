using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.BL.Service;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Repository;
using MySqlConnector;
using System.Data;
using System.Data.SqlTypes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher2023.Demo.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController : BaseController<EmployeeDto, EmployeeUpdateDto>
    {
        protected readonly IEmployeeService _employeeService;
        public EmployeeController(
            IEmployeeService employeeService
            ) : base(employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// API kiểm tra employee code đã tồn tại chưa
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("is-existed-code")]
        [HttpGet]
        public async Task<bool> CheckIsExistedCode(string code)
        {
            // Tạo connection
            return await _employeeService.CheckEmployeeCode(code);
        }

        /// <summary>
        /// API lọc trang theo mã nhân viên, họ và tên
        /// </summary>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="pageNumber">Thứ tự của trang</param>
        /// <param name="employeeFilter">Từ khóa để lọc</param>
        /// <returns>Tổng số bản ghi, danh sách nhân viên</returns>
        // GET: api/employees/filter
        [Route("filter")]
        [HttpGet]
        public async Task<EmployeePageDto> GetPage(int pageSize, int pageNumber, string? employeeFilter)
        {
            return await _employeeService.GetPage(pageSize, pageNumber, employeeFilter);
        }

        /// <summary>
        /// API lấy mã nhân viên mới 
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        // GET api/v1/Employees/NewEmployeeCode
        [Route("NewEmployeeCode")]
        [HttpGet]
        public async Task<string> GetNewEmployeeCode()
        {
            return await _employeeService.GetNewEmployeeCode();
        }

        /// <summary>
        /// API thêm một nhân viên 
        /// </summary>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns>Mã kết quả</returns>
        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<int> Post(Employee employee)
        {
            return await _employeeService.PostAsync(employee);
        }

        /// <summary>
        /// API sửa thông tin nhân viên 
        /// </summary>
        /// <param name="id">Mã của nhân viên cần sửa </param>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns>Mã lỗi trả về</returns>
        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            return Ok(await _baseService.UpdateAsync(id, employeeUpdateDto));
            

        }

        /// <summary>
        /// API xóa một nhân viên
        /// </summary>
        /// <param name="id">Mã của nhân viên cần xóa </param>
        /// <returns>Mã lỗi trả về</returns>
        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public override async Task<int> DeleteAsync(Guid id)
        {
            return await _employeeService.DeleteAsync(id);
        }
    }
}