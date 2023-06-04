using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Entity
{
    /// <summary>
    /// Class phòng ban
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public class Department : BaseEntity
    {
        /// <summary>
        /// Id phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public string? DepartmentName { get; set; }
    }
}
