using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.BL.Service;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;
using ClosedXML.Excel;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.Common.Resource;

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
        /// <param name="code">Mã nhân viên</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        [Route("is-code-duplicated")]
        [HttpGet]
        public async Task<IActionResult> CheckIsExistedCodeAsync(string code)
        {
            // Tạo connection
            return Ok(await _employeeService.CheckEmployeeCodeAsync(code));
        }

        /// <summary>
        /// API kiểm tra trùng mã sửa ngoại trừ mã cũ 
        /// </summary>
        /// <param name="employeeCode">Mã cần kiểm tra</param>
        /// <param name="itsCode">Mã trước khi sửa</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        [Route("is-edit-code-duplicated")]
        [HttpGet]
        public async Task<IActionResult> CheckDuplicatedEmployeeEditCodeAsync(string employeeCode, string itsCode)
        {
            // Tạo connection
            return Ok(await _employeeService.CheckDuplicatedEmployeeEditCodeAsync(employeeCode, itsCode));
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
        public async Task<IActionResult> GetPageAsync([FromQuery] EmployeePageArgument employeePageArgument)
        {
            // Trả về trang nhân viên
            return Ok(await _employeeService.GetPageAsync(employeePageArgument));
        }

        /// <summary>
        /// API lấy mã nhân viên mới 
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>%
        /// Author: LeDucTiep (23/05/2023)
        // GET api/v1/Employees/new-employee-code
        [Route("new-employee-code")]
        [HttpGet]
        public async Task<IActionResult> GetNewEmployeeCodeAsync()
        {
            return Ok(await _employeeService.GetNewEmployeeCodeAsync());
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
            if (employeeCreateDto == null)
            {
                throw new BadRequestException();
            }
            EmployeeDto employee = await _employeeService.PostAsync(employeeCreateDto);

            return StatusCode(201, employee.EmployeeId);
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

            using MemoryStream ms = new();

            // Lưu file vào MemoryStream
            xlWorkbook.SaveAs(ms);

            // Gửi file
            return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ExportExcelResource.FileName);
        }

        /// <summary>
        /// API xuất tất cả nhân viên 
        /// </summary>
        /// <returns>Danh sách tất cả nhân viên</returns>
        /// Author: LeDucTiep (23/05/2023)
        [HttpGet("exporting-json")]
        public async Task<IActionResult> ExportJsonAsync()
        {
            IEnumerable<EmployeeExport> employees = await _employeeService.ExportJsonAsync();
            if (employees == null || !employees.Any())
                return NoContent();
            return Ok(employees);
        }

        #endregion
    }
}
