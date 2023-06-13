using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.Resource
{
    /// <summary>
    /// Resource nội dung lỗi 
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public static class InternalDevMessage
    {
        /// <summary>
        /// Lỗi không tìm thấy phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string ConnectDbError = "Không kết nối được với cơ sở dữ liệu";
    }

    /// <summary>
    /// Thông tin lỗi cho người dùng
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public static class InternalUserMessage
    {
        /// <summary>
        /// Lỗi không tìm thấy phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string ConnectDbError = "Có lỗi xảy ra vui lòng liên hệ Misa để được hỗ trợ";

        /// <summary>
        /// Lỗi không xác định
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string Unknown = "Có lỗi xảy ra vui lòng liên hệ Misa để được hỗ trợ";
    }

    /// <summary>
    /// Thông tin lỗi phòng ban 
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public static class DepartmentUserMessage
    {
        /// <summary>
        /// Lỗi không tìm thấy Id phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdNotFound = "Không tìm thấy phòng ban";
    }
    /// <summary>
    /// Thông tin lỗi phòng ban 
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public static class DepartmentDevMessage
    {
        /// <summary>
        /// Lỗi không tìm thấy Id phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdNotFound = "Không tìm thấy departmnetId";
    }


    /// <summary>
    /// Thông tin lỗi chức vụ
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public static class PositionUserMessage
    {
        /// <summary>
        /// Lỗi không tìm thấy Id chức vụ
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdNotFound = "Chức vụ không tồn tại";
    }
    /// <summary>
    /// Thông tin lỗi chức vụ
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public static class PositionDevMessage
    {
        /// <summary>
        /// Lỗi không tìm thấy Id chức vụ
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdNotFound = "Không tìm thấy positionId";
    }

    /// <summary>
    /// Thông tin lỗi phân trang
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public static class PagingUserMessage
    {
        /// <summary>
        /// Lỗi kích thước trang không hợp lệ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string InvalidPageSize = "Kích thước trang phải lớn hơn 0";

        /// <summary>
        /// Lỗi số thứ tự trang không hợp lệ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string InvalidPageNumber = "số trang phải lớn hơn 0";

        /// <summary>
        /// Lỗi độ dài từ khóa tìm kiếm
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string EmployeeSearchTermTooLong = "Từ khóa tìm kiếm không được dài quá 255 kí tự";
    }

    /// <summary>
    /// Thông tin lỗi phân trang
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public static class PagingDevMessage
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
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string EmployeeSearchTermTooLong = "EmployeeSearchTerm không được dài quá 255 kí tự";
    }

    /// <summary>
    /// Thông tin lỗi nhân viên
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public static class EmployeeUserMessage
    {
        /// <summary>
        /// Lỗi không tìm thấy Id nhân viên
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdNotFound = "Nhân viên không tồn tại";

        /// <summary>
        /// Lỗi trùng mã nhân viên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string CodeDuplicated = "Mã nhân viên đã tồn tại";

        /// <summary>
        /// Lỗi để trống mã nhân viên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string CodeIsRequired = "Mã nhân viên không được để trống";

        /// <summary>
        /// Lỗi để trống tên nhân viên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string FullNameIsRequired = "Tên không được để trống";

        /// <summary>
        /// Mã nhân viên không được dài quá 20 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string EmployeeCodeTooLong = "Mã nhân viên không được dài quá 20 ký tự";

        /// <summary>
        /// Họ tên không được dài quá 100 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string FullNameTooLong = "Họ tên không được dài quá 100 ký tự";

        /// <summary>
        /// Email không được dài quá 50 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string EmailTooLong = "Email không được dài quá 50 ký tự";

        /// <summary>
        /// Địa chỉ không được dài quá 255 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string AddressTooLong = "Địa chỉ không được dài quá 255 ký tự";

        /// <summary>
        /// Số điện thoại không được dài quá 50 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string PhoneNumberTooLong = "Số điện thoại không được dài quá 50 ký tự";

        /// <summary>
        /// Số chứng minh nhân dân không được dài quá 25 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdentityNumberTooLong = "Số chứng minh nhân dân không được dài quá 25 ký tự";

        /// <summary>
        /// Nơi cấp không được dài quá 255 ký tự 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdentityPlaceTooLong = "Nơi cấp không được dài quá 255 ký tự ";

        /// <summary>
        /// Tài khoản ngân hàng không được dài quá 25 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string BankAccountNumberTooLong = "Tài khoản ngân hàng không được dài quá 25 ký tự";

        /// <summary>
        /// Tên ngân hàng không được dài quá 255 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string NameOfBankTooLong = "Tên ngân hàng không được dài quá 255 ký tự";

        /// <summary>
        /// Chi nhánh ngân hàng không được dài quá 255 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string BankAccountBranchTooLong = "Chi nhánh ngân hàng không được dài quá 255 ký tự";

        /// <summary>
        /// Ngày sinh không được phép lớn hơn ngày hiện tại
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string DateOfBirthInvalidTime = "Ngày sinh không được lớn hơn ngày hiện tại";

        /// <summary>
        /// Ngày cấp không được phép lớn hơn ngày hiện tại
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdentityDateInvalidTime = "Ngày cấp không được lớn hơn ngày hiện tại";

        /// <summary>
        /// Email không đúng định dạng
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string EmailInvalid = "Email không đúng định dạng";

        /// <summary>
        /// Guid không đúng định dạng
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string GuidInvalid = "Có lỗi xảy ra vui lòng liên hệ Misa để được hỗ trợ";
    }

    /// <summary>
    /// Thông tin lỗi nhân viên
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public static class EmployeeDevMessage
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

        /// <summary>
        /// Lỗi để trống mã nhân viên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string CodeIsRequired = "Mã nhân viên không được để trống";

        /// <summary>
        /// Lỗi để trống tên nhân viên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string FullNameIsRequired = "Tên không được để trống";

        /// <summary>
        /// Mã nhân viên không được dài quá 20 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string EmployeeCodeTooLong = "Mã nhân viên không được dài quá 20 ký tự";

        /// <summary>
        /// Họ tên không được dài quá 100 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string FullNameTooLong = "Họ tên không được dài quá 100 ký tự";

        /// <summary>
        /// Email không được dài quá 50 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string EmailTooLong = "Email không được dài quá 50 ký tự";

        /// <summary>
        /// Địa chỉ không được dài quá 255 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string AddressTooLong = "Địa chỉ không được dài quá 255 ký tự";

        /// <summary>
        /// Số điện thoại không được dài quá 50 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string PhoneNumberTooLong = "Số điện thoại không được dài quá 50 ký tự";

        /// <summary>
        /// Số chứng minh nhân dân không được dài quá 25 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdentityNumberTooLong = "Số chứng minh nhân dân không được dài quá 25 ký tự";

        /// <summary>
        /// Nơi cấp không được dài quá 255 ký tự 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdentityPlaceTooLong = "Nơi cấp không được dài quá 255 ký tự ";

        /// <summary>
        /// Tài khoản ngân hàng không được dài quá 25 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string BankAccountNumberTooLong = "Tài khoản ngân hàng không được dài quá 25 ký tự";

        /// <summary>
        /// Tên ngân hàng không được dài quá 255 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string NameOfBankTooLong = "Tên ngân hàng không được dài quá 255 ký tự";

        /// <summary>
        /// Chi nhánh ngân hàng không được dài quá 255 ký tự
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string BankAccountBranchTooLong = "Chi nhánh ngân hàng không được dài quá 255 ký tự";

        /// <summary>
        /// Ngày sinh không được phép lớn hơn ngày hiện tại
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string DateOfBirthInvalidTime = "Ngày sinh không được lớn hơn ngày hiện tại";

        /// <summary>
        /// Ngày cấp không được phép lớn hơn ngày hiện tại
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string IdentityDateInvalidTime = "Ngày cấp không được lớn hơn ngày hiện tại";

        /// <summary>
        /// Email không đúng định dạng
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string EmailInvalid = "Email không đúng định dạng";

        /// <summary>
        /// Guid không đúng định dạng
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string GuidInvalid = "Id không đúng định dạng";
        
    }
}
