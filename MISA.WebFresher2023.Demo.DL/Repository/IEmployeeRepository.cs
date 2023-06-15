using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;

namespace MISA.WebFresher2023.Demo.DL.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Hàm kiểm tra đã tồn tại mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task<bool> CheckExistedEmployeeCodeAsync(string employeeCode);

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task<string> GetNewEmployeeCodeAsync();

        /// <summary>
        /// Hàm phân trang
        /// </summary>
        /// <param name="pageSize">Kích thước trang </param>
        /// <param name="pageNumber">Số thứ tự trang </param>
        /// <param name="employeeSearchTerm">Từ khóa tìm kiếm</param>
        /// <returns>EmployeePage</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task<EmployeePage> GetPageAsync(EmployeePageArgument employeePageArgument);

        /// <summary>
        /// Hàm lấy dữ liệu để xuất employee 
        /// </summary>
        /// <returns>EmployeeExport</returns>
        /// Author: LeDucTiep (07/06/2023)
        Task<IEnumerable<EmployeeExport>> GetEmployeeExportAsync();

        /// <summary>
        /// Hàm kiểm tra mã EmployeeCode muốn sửa đã tồn tại chưa
        /// </summary>
        /// <param name="employeeCode">EmployeeCode</param>
        /// <param name="itsCode">EmployeeCode trước khi sửa</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task<bool> CheckDuplicatedEmployeeEditCodeAsync(string employeeCode, string itsCode);

        /// <summary>
        /// Hàm kiểm tra mã EmployeeCode muốn sửa đã tồn tại chưa
        /// </summary>
        /// <param name="employeeCode">EmployeeCode</param>
        /// <param name="itsId">Id của bản ghi</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task<bool> CheckDuplicatedEmployeeEditCodeAsync(string employeeCode, Guid itsId);
    }
}
