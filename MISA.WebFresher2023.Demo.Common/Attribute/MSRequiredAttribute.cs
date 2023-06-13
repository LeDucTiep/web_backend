using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.Attribute
{
    /// <summary>
    /// Attribute trường bắt buộc 
    /// </summary>
    /// Author: LeDucTiep (11/06/2023)
    [AttributeUsage(AttributeTargets.All)]
    public class MSRequiredAttribute : System.Attribute
    {
        /// <summary>
        /// Mã lỗi nội bộ 
        /// </summary>
        /// Author: LeDucTiep (11/06/2023)
        public int ErrorCode { get; set; }
    }
}
