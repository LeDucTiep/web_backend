using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.BL.Service;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;
using System.Text.RegularExpressions;
using ClosedXML.Excel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher2023.Demo.Controllers
{
    [Route("api/v1/[controller]s")]
    public class EmployeeController : BaseController<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        #region Field
        /// <summary>
        /// EmployeeService
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        protected readonly IEmployeeService _employeeService;
        #endregion

        #region Contructor
        public EmployeeController(
            IEmployeeService employeeService
            ) : base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        #region Method
        /// <summary>
        /// API kiểm tra employee code đã tồn tại chưa
        /// </summary>
        /// <param name="code"></param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        [Route("is-existed-code")]
        [HttpGet]
        public async Task<bool> CheckIsExistedCodeAsync(string code)
        {
            // Xóa khoảng trắng
            code = Regex.Replace(code, @"\s+", "");
            // Tạo connection
            return await _employeeService.CheckEmployeeCode(code);
        }

        /// <summary>
        /// API lọc trang theo mã nhân viên, họ và tên
        /// </summary>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="pageNumber">Thứ tự của trang</param>
        /// <param name="employeeSearchTerm">Từ khóa để lọc</param>
        /// <returns>Tổng số bản ghi, danh sách nhân viên</returns>
        /// Author: LeDucTiep (23/05/2023)
        // GET: api/employees/paging
        [Route("paging")]
        [HttpGet]
        public async Task<EmployeePage> GetPageAsync(int pageSize, int pageNumber, string? employeeSearchTerm)
        {
            // Trả về trang nhân viên
            return await _employeeService.GetPageAsync(pageSize, pageNumber, employeeSearchTerm);
        }

        /// <summary>
        /// API lấy mã nhân viên mới 
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Author: LeDucTiep (23/05/2023)
        // GET api/v1/Employees/new-employee-code
        [Route("new-employee-code")]
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
            EmployeeDto employee = await _employeeService.PostAsync(employeeCreateDto);
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
            return StatusCode(204);
        }

        /// <summary>
        /// API xuất file chứa thông tin tất cả nhân viên 
        /// </summary>
        /// <returns>File excel xuất thông tin tất cả nhân viên </returns>
        /// Author: LeDucTiep (23/05/2023)
        [HttpGet("exporting-excel")]
        public async Task<ActionResult> ExportExcelAsync()
        {
            // Tạo file excel 
            XLWorkbook xlWorkbook = await _employeeService.ExportExcelAsync();

            using (MemoryStream ms = new MemoryStream())
            {
                // Lưu file vào MemoryStream
                xlWorkbook.SaveAs(ms);

                // Gửi file
                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_nhan_vien.xlsx");
            }
        }

        /// <summary>
        /// API xuất tất cả nhân viên 
        /// </summary>
        /// <returns>Danh sách tất cả nhân viên</returns>
        /// Author: LeDucTiep (23/05/2023)
        [HttpGet("exporting-json")]
        public async Task<IEnumerable<EmployeeExport>> ExportJsonAsync()
        {
            return await _employeeService.ExportJsonAsync();
        }

        #endregion
    }
}
