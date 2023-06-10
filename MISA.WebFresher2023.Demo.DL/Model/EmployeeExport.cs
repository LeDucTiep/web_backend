using MISA.WebFresher2023.Demo.Common.Attribute;
using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Model
{
    /// <summary>
    /// Class thông tin nhân viên xuất file 
    /// </summary>
    /// Author: LeDucTiep (07/06/2023)
    public class EmployeeExport
    {
        /// <summary>
        /// Mã nhân viên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [MSRequired(ErrorCode = (int)EmployeeErrorCode.CodeIsRequired)]
        [MSMaxLength(Length = 20, ErrorCode = (int)EmployeeErrorCode.EmployeeCodeTooLong)]
        public string EmployeeCode { get; set; }

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
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Tên chức vụ
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [MaxLength(255)]
        public string? PositionName { get; set; }

        /// <summary>
        /// Tên phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [MaxLength(255)]
        public string? DepartmentName { get; set; }

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
    }
}
