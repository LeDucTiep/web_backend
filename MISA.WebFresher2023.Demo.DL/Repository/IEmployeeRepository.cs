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
        public Task<bool> CheckEmployeeCode(string employeeCode);

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Author: LeDucTiep (23/05/2023)
        public Task<string> GetNewEmployeeCode();

        /// <summary>
        /// Hàm phân trang
        /// </summary>
        /// <param name="pageSize">Kích thước trang </param>
        /// <param name="pageNumber">Số thứ tự trang </param>
        /// <param name="employeeFilter">Từ khóa tìm kiếm</param>
        /// <returns>EmployeePage</returns>
        /// Author: LeDucTiep (23/05/2023)
        public Task<EmployeePage> GetPage(int pageSize, int pageNumber, string? employeeFilter);

        /// <summary>
        /// Hàm thêm một bản ghi 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Mã lỗi</returns>
        /// Author: LeDucTiep (23/05/2023)
        public new Task<int> PostAsync(Employee employee);
    }
}
