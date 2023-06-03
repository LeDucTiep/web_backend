using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.Resource
{
    public static class ResourceException
    {
        /// <summary>
        /// Lỗi không tìm thấy phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string NotFoundDepartment = "Không tồn tại departmentId";

        /// <summary>
        /// Lỗi không tìm thấy chức vụ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string NotFoundPosition = "Không tồn tại positionId";
    }

    /// <summary>
    /// Thông tin lỗi cho người dùng
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public static class UserMessage
    {
        /// <summary>
        /// Lỗi tham số phân trang 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string PagingArgumentError = "Có lỗi xảy ra vui lòng liên hệ Misa để được hỗ trợ";

        /// <summary>
        /// Lỗi nội bộ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string InternalError = "Có lỗi xảy ra vui lòng liên hệ Misa để được hỗ trợ";

        /// <summary>
        /// Lỗi không tìm thấy
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string NotFoundError = "Có lỗi xảy ra vui lòng liên hệ Misa để được hỗ trợ";

        /// <summary>
        /// Lỗi mã nhân viên đã tồn tại 
        /// </summary>
        /// Author: LeDucTiep (27/05/2023)
        public static readonly string ExistedEmployeeCode = "Mã nhân viên đã tồn tại";
    }
    public static class DepartmentErrorMessage
    {
        /// <summary>
        /// Lỗi không tìm thấy Id phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdNotFound = "Không tìm thấy departmnetId";
    }

    public static class PositionErrorMessage
    {
        /// <summary>
        /// Lỗi không tìm thấy Id chức vụ
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdNotFound = "Không tìm thấy positionId";
    }

    public static class PagingErrorMessage
    {
        /// <summary>
        /// Lỗi kích thước trang không hợp lệ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string InvalidPageSize = "PageSize phải lớn hơn 0";

        /// <summary>
        /// Lỗi số thứ tự trang không hợp lệ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string InvalidPageNumber = "PageNumber phải lớn hơn 0";

        /// <summary>
        /// Lỗi độ dài từ khóa tìm kiếm
        /// </summary>
        public static readonly string InvalidEmployeeSearchTerm = "Employee không được dài quá 255 kí tự";
    }

    public static class EmployeeErrorMessage
    {
        /// <summary>
        /// Lỗi không tìm thấy Id nhân viên
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdNotFound = "Không tìm thấy employeeId";

        /// <summary>
        /// Lỗi trùng mã nhân viên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string CodeDuplicated = "EmployeeCode đã tồn tại";
    }
}
