using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Dto
{
    /// <summary>
    /// Class chuyển đổi dữ liệu để sửa thông tin phòng ban 
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public class DepartmentUpdateDto
    {
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public string? DepartmentName { get; set; }
    }
}
