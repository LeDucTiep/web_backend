using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.Constant
{

    /// <summary>
    /// Internal mã lỗi
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public enum InternalErrorCode
    {
        /// <summary>
        /// Lỗi không xác định
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        Unknown = 5001,

        /// <summary>
        /// Lỗi không kết nối được database
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        ConnectDbError = 5002,
    }

    /// <summary>
    /// Department mã lỗi
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public enum DepartmentErrorCode
    {
        /// <summary>
        /// Lỗi không tìm thấy Id phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        IdNotFound = 4001,
    }

    /// <summary>
    /// Position mã lỗi 
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public enum PositionErrorCode
    {
        /// <summary>
        /// Lỗi không tìm thấy Id chức vụ
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        IdNotFound = 3001,
    }

    /// <summary>
    /// Paging mã lỗi 
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public enum PagingErrorCode
    {
        /// <summary>
        /// Lỗi kích thước trang không hợp lệ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        InvalidPageSize = 2001,

        /// <summary>
        /// Lỗi số thứ tự trang không hợp lệ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        InvalidPageNumber = 2002,

        /// <summary>
        /// Lỗi độ dài từ khóa tìm kiếm
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        EmployeeSearchTermTooLong = 2003,
    }

    /// <summary>
    /// Employee mã lỗi 
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
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

        /// <summary>
        /// Không để trống mã nhân viên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        CodeIsRequired = 1003,

        /// <summary>
        /// Không để trống tên nhân viên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        FullNameIsRequired = 1004,

        /// <summary>
        /// Mã nhân viên quá dài 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        EmployeeCodeTooLong = 1005,

        /// <summary>
        /// Họ tên quá dài 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        FullNameTooLong = 1006,

        /// <summary>
        /// Email quá dài 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        EmailTooLong = 1007,

        /// <summary>
        /// Địa chỉ quá dài
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        AddressTooLong = 1008,

        /// <summary>
        /// Số điện thoại quá dài
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        PhoneNumberTooLong = 1009,

        /// <summary>
        /// Số chứng minh nhân dân quá dài 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        IdentityNumberTooLong = 1010,

        /// <summary>
        /// Nơi cấp chứng minh nhân dân quá dài 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        IdentityPlaceTooLong = 1011,

        /// <summary>
        /// Tài khoản ngân hàng quá dài 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        BankAccountNumberTooLong = 1012,

        /// <summary>
        /// Tên ngân hàng quá dài 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        NameOfBankTooLong = 1013,

        /// <summary>
        /// Chi nhánh ngân hàng quá dài 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        BankAccountBranchTooLong = 1014,

        /// <summary>
        /// Ngày sinh không được phép lớn hơn ngày hiện tại
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        DateOfBirthInvalidTime = 1015,

        /// <summary>
        /// Ngày cấp không được phép lớn hơn ngày hiện tại
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        IdentityDateInvalidTime = 1016,

        /// <summary>
        /// Email không đúng
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        EmailInvalid = 1017,

        /// <summary>
        /// Guid không đúng
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        GuidInvalid = 1018,
    }
}
