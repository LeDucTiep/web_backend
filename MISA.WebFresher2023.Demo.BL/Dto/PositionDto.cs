using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Dto
{
    public class PositionDto
    {
        /// <summary>
        /// Id chức vụ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public Guid PositionId { get; set; }

        /// <summary>
        /// Tên chức vụ 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public string? PositionName { get; set; }
    }
}
