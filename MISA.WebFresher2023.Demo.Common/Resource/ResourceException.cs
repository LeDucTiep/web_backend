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
    }
}
