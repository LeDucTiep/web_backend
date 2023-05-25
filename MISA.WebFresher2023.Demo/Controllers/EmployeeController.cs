﻿using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.BL.Service;
using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.Common.Resource;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher2023.Demo.Controllers
{
    [Route("api/v1/[controller]s")]
    public class EmployeeController : BaseController<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
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
        /// Author: LeDucTiep (23/05/2023)
        [Route("is-existed-code")]
        [HttpGet]
        public async Task<bool> CheckIsExistedCodeAsync(string code)
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
        /// Author: LeDucTiep (23/05/2023)
        // GET: api/employees/filter
        [Route("filter")]
        [HttpGet]
        public async Task<EmployeePage> GetPageAsync(int pageSize, int pageNumber, string? employeeFilter)
        {
            if (pageSize <= 0)
            {
                throw new PagingArgumentException("EmployeeController.GetPageAsync", ErrorCodeConst.PagingInvalidPageSize);
            }
            if (pageNumber <= 0)
            {
                throw new PagingArgumentException("EmployeeController.GetPageAsync", ErrorCodeConst.PagingInvalidPageNumber);
            }

            return await _employeeService.GetPage(pageSize, pageNumber, employeeFilter);
        }

        /// <summary>
        /// API lấy mã nhân viên mới 
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Author: LeDucTiep (23/05/2023)
        // GET api/v1/Employees/NewEmployeeCode
        [Route("NewEmployeeCode")]
        [HttpGet]
        public async Task<string> GetNewEmployeeCodeAsync()
        {
            return await _employeeService.GetNewEmployeeCode();
        }

        /// <summary>
        /// API thêm một nhân viên 
        /// </summary>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns>Mã kết quả</returns>
        /// Author: LeDucTiep (23/05/2023)
        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<IActionResult> PostAsync(EmployeeCreateDto employeeCreateDto)
        {
            Employee employee = await _employeeService.PostAsync(employeeCreateDto);
            return StatusCode(200, employee.EmployeeId);
        }

        /// <summary>
        /// API sửa thông tin nhân viên 
        /// </summary>
        /// <param name="id">Mã của nhân viên cần sửa </param>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns>Mã lỗi trả về</returns>
        /// Author: LeDucTiep (23/05/2023)
        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, EmployeeUpdateDto employeeUpdateDto)
        {
            await _baseService.UpdateAsync(id, employeeUpdateDto);
            return StatusCode(200,
                        new
                        {

                        });
        }
    }
}