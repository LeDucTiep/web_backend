using MISA.WebFresher2023.Demo.Common.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.MyException
{
    public class BaseException
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public int ErrorCode { get; set; }

        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public string? UserMessage { get; set; }

        /// <summary>
        /// Thông báo cho dev 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public string? DevMessage { get; set; }

        /// <summary>
        /// Id để truy vết 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public string? TraceId { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public string? MoreInfo { get; set; }

        /// <summary>
        /// Chuyển sang dạng json
        /// </summary>
        /// <returns>Chuỗi json</returns>
        /// Author: LeDucTiep (23/05/2023)
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
