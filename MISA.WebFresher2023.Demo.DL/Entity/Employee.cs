using MISA.WebFresher2023.Demo.Common.Attribute;
using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Enum;
using System.Text.RegularExpressions;

namespace MISA.WebFresher2023.Demo.DL.Entity
{
    /// <summary>
    /// Class nhân viên
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public class Employee : BaseEntity
    {
        private string employeeCode;

        /// <summary>
        /// Id phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [MSRequired(ErrorCode = (int)EmployeeErrorCode.CodeIsRequired)]
        [MSMaxLength(Length = 20, ErrorCode = (int)EmployeeErrorCode.EmployeeCodeTooLong)]
        public string EmployeeCode { get => employeeCode; set => employeeCode = Regex.Replace(value, @"\s+", ""); }

        /// <summary>
        /// Họ và tên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [MSRequired(ErrorCode = (int)EmployeeErrorCode.FullNameIsRequired)]
        [MSMaxLength(Length = 100, ErrorCode = (int)EmployeeErrorCode.FullNameTooLong)]
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public Gender? Gender { get; set; }

        /// <summary>
        /// Ngày sinh 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [MSValidDateInThePast(ErrorCode = (int)EmployeeErrorCode.DateOfBirthInvalidTime)]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [MSMaxLength(Length = 50, ErrorCode = (int)EmployeeErrorCode.EmailTooLong)]
        [MSEmail(ErrorCode = (int)EmployeeErrorCode.EmailInvalid)]
        public string? Email { get; set; }

        /// <summary>
        /// Địa chỉ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [MSMaxLength(Length = 255, ErrorCode = (int)EmployeeErrorCode.AddressTooLong)]
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [MSMaxLength(Length = 50, ErrorCode = (int)EmployeeErrorCode.PhoneNumberTooLong)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [MSMaxLength(Length = 25, ErrorCode = (int)EmployeeErrorCode.IdentityNumberTooLong)]
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp chứng minh thư 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [MSValidDateInThePast(ErrorCode = (int)EmployeeErrorCode.IdentityDateInvalidTime)]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi chấp chứng minh thư 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [MSMaxLength(Length = 255, ErrorCode = (int)EmployeeErrorCode.IdentityPlaceTooLong)]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Id vị trí, chức vụ
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        /// Author: LeDucTiep (04/06/2023)
        [MSMaxLength(Length = 25, ErrorCode = (int)EmployeeErrorCode.BankAccountNumberTooLong)]
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        /// Author: LeDucTiep (04/06/2023)
        [MSMaxLength(Length = 255, ErrorCode = (int)EmployeeErrorCode.NameOfBankTooLong)]
        public string? NameOfBank { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        /// Author: LeDucTiep (04/06/2023)
        [MSMaxLength(Length = 255, ErrorCode = (int)EmployeeErrorCode.BankAccountBranchTooLong)]
        public string? BankAccountBranch { get; set; }
    }
}
