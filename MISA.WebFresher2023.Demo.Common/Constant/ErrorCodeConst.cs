using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.Constant
{
    /// <summary>
    /// Enum mã lỗi
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public enum DepartmentErrorCode
    {
        /// <summary>
        /// Lỗi không tìm thấy Id phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        IdNotFound = 1003,
    }

    public enum PositionErrorCode
    {
        /// <summary>
        /// Lỗi không tìm thấy Id chức vụ
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        IdNotFound = 1004,
    }

    public enum PagingErrorCode
    {
        /// <summary>
        /// Lỗi kích thước trang không hợp lệ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        InvalidPageSize = 1005,

        /// <summary>
        /// Lỗi số thứ tự trang không hợp lệ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        InvalidPageNumber = 1006,

        /// <summary>
        /// Lỗi độ dài từ khóa tìm kiếm
        /// </summary>
        InvalidEmployeeSearchTerm = 1007,
    }

    public enum EmployeeErrorCode
    {
        /// <summary>
        /// Lỗi không tìm thấy Id nhân viên
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        IdNotFound = 1001,

        /// <summary>
        /// Lỗi trùng mã nhân viên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        CodeDuplicated = 1002,
    }
}
