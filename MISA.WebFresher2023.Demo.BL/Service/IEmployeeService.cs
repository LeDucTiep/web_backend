using ClosedXML.Excel;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Model;

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
        Task<bool> CheckEmployeeCodeAsync(string employeeCode);

        /// <summary>
        /// Hàm lấy danh sách nhân viên theo trang
        /// </summary>
        /// <param name="pageSize">Kích thước trang </param>
        /// <param name="pageNumber">Số thứ tự trang </param>
        /// <param name="employeeSearchTerm">Từ khóa tìm kiếm </param>
        /// <returns>Trang nhân viên</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task<EmployeePage> GetPageAsync(EmployeePageArgument employeePageArgument);

        /// <summary>
        /// Lấy mã nhân viên mới 
        /// </summary>
        /// <returns>Mã nhân viên mới </returns>
        /// Author: LeDucTiep (23/05/2023)
        Task<string> GetNewEmployeeCodeAsync();

        /// <summary>
        /// Hàm lấy danh sách employee và tạo ra file excel
        /// </summary>
        /// <returns>XLWorkbook</returns>
        /// Author: LeDucTiep (07/06/2023)
        Task<XLWorkbook> ExportExcelAsync();

        /// <summary>
        /// Hàm lấy tất cả employee để xuất file 
        /// </summary>
        /// <returns>Danh sách employee</returns>
        /// Author: LeDucTiep (07/06/2023)
        Task<IEnumerable<EmployeeExport>> ExportJsonAsync();

        /// <summary>
        /// Hàm kiểm tra mã EmployeeCode muốn sửa đã tồn tại chưa
        /// </summary>
        /// <param name="employeeCode">EmployeeCode</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task<bool> CheckDuplicatedEmployeeEditCodeAsync(string employeeCode, string itsCode);
    }
}
