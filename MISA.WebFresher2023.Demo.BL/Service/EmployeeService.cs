using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.Common.Resource;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;
using MISA.WebFresher2023.Demo.DL.Repository;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>, IEmployeeService
    {
        #region Field
        public IEmployeeRepository _employeeRepository; 
        #endregion

        #region Contructor
        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper) : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region Method
        /// <summary>
        /// Hàm kiểm tra mã nhân viên đã tồn tại chưa
        /// </summary>
        /// <param name="code">Mã nhân viên</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<bool> CheckEmployeeCode(string code)
        {
            return await _employeeRepository.CheckEmployeeCode(code);
        }

        /// <summary>
        /// Hàm phân lấy danh sách nhân viên theo trang 
        /// </summary>
        /// <param name="pageSize">Kích thước trang </param>
        /// <param name="pageNumber">Số thứ tự trang</param>
        /// <param name="employeeSearchTerm">Từ khóa tìm kiếm</param>
        /// <returns>EmployeePage</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<EmployeePage> GetPageAsync(int pageSize, int pageNumber, string? employeeSearchTerm)
        {
            // Lỗi kích thước trang 
            if (pageSize <= 0)
            {
                throw new PagingArgumentException(PagingErrorMessage.InvalidPageSize, (int)PagingErrorCode.InvalidPageSize);
            }

            // Lỗi số thứ tự trang 
            if (pageNumber <= 0)
            {
                throw new PagingArgumentException(PagingErrorMessage.InvalidPageNumber,(int)PagingErrorCode.InvalidPageNumber);
            }

            // Lỗi độ dài từ khóa tìm kiếm
            if(employeeSearchTerm != null && employeeSearchTerm.Length > 255)
            {
                throw new PagingArgumentException(PagingErrorMessage.InvalidEmployeeSearchTerm,(int)PagingErrorCode.InvalidEmployeeSearchTerm);
            }

            return await _employeeRepository.GetPageAsync(pageSize, pageNumber, employeeSearchTerm);
        }

        /// <summary>
        /// Hàm lấy mã nhân viên mới 
        /// </summary>
        /// <returns>Mã nhân viên mới </returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<string> GetNewEmployeeCode()
        {
            return await _employeeRepository.GetNewEmployeeCode();
        }
        #endregion
    }
}
