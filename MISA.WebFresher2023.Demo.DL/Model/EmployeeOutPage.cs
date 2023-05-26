using MISA.WebFresher2023.Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Model
{
    public class EmployeeOutPage : Employee
    {
        /// <summary>
        /// Tên chức vụ
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public string? PositionName { get; set; }

        /// <summary>
        /// Tên phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public string? DepartmentName { get; set; }
    }
}
