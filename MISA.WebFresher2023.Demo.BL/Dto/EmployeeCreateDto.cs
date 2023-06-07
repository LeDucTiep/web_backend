using MISA.WebFresher2023.Demo.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Dto
{
    /// <summary>
    /// Class chuyển đổi dữ liệu sang nhân viên để thêm
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public class EmployeeCreateDto
    {
        /// <summary>
        /// Mã nhân viên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [StringLength(20)]
        public string employeeCode;
        public string EmployeeCode
        {
            get { return employeeCode; }
            set { employeeCode = Regex.Replace(value, @"\s+", ""); }
        }

        /// <summary>
        /// Họ và tên 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [StringLength(100)]
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
        /// Email
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [StringLength(50)]
        public string? Email { get; set; }

        /// <summary>
        /// Địa chỉ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [StringLength(255)]
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [StringLength(50)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [StringLength(25)]
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp chứng minh thư 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi chấp chứng minh thư 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        [StringLength(255)]
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
        [StringLength(25)]
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        /// Author: LeDucTiep (04/06/2023)
        [StringLength(255)]
        public string? NameOfBank { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        /// Author: LeDucTiep (04/06/2023)
        [StringLength(255)]
        public string? BankAccountBranch { get; set; }
    }
}
