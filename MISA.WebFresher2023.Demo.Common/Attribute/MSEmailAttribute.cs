using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.Attribute
{
    /// <summary>
    /// Attribute kiểm tra chuỗi là gmail
    /// </summary>
    /// Author: LeDucTiep (11/06/2023)
    [AttributeUsage(AttributeTargets.All)]
    public class MSEmailAttribute : System.Attribute
    {
        /// <summary>
        /// Mã lỗi nội bộ 
        /// </summary>
        /// Author: LeDucTiep (11/06/2023)
        public int ErrorCode { get; set; }

        /// <summary>
        /// Là hợp lệ 
        /// </summary>
        /// <param name="value">Chuỗi gmail</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (11/06/2023)
        public static bool IsValid(object value)
        {
            if (value == null) return true;

            try
            {
                // Kiểm tra chuỗi là gmail
                string email = (string)value;

                string regex = "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$";
                var match = Regex.Match(email, regex, RegexOptions.IgnoreCase);

                return match.Success;
            }
            catch { return true; }
        }
    }
}
