using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.Attribute
{
    /// <summary>
    /// Attribute kiểm tra độ dài chuỗi 
    /// </summary>
    /// Author: LeDucTiep (11/06/2023)
    [AttributeUsage(AttributeTargets.All)]
    public class MSMaxLengthAttribute : System.Attribute
    {
        /// <summary>
        /// Mã lỗi nội bộ 
        /// </summary>
        /// Author: LeDucTiep (11/06/2023)
        public int ErrorCode { get; set; }

        /// <summary>
        /// Độ dài tối đa 
        /// </summary>
        /// Author: LeDucTiep (11/06/2023)
        public int Length { get; set; }
    }
}
