using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.Attribute
{
    /// <summary>
    /// Attribute ngày tháng trong quá khứ là hợp lệ
    /// </summary>
    /// Author: LeDucTiep (11/06/2023)
    [AttributeUsage(AttributeTargets.All)]
    public class MSValidDateInThePastAttribute : System.Attribute
    {
        /// <summary>
        /// Mã lỗi nội bộ 
        /// </summary>
        /// Author: LeDucTiep (11/06/2023)
        public int ErrorCode { get; set; }

        /// <summary>
        /// Là hợp lệ
        /// </summary>
        /// <param name="date">Ngày tháng</param>
        /// <returns>bool</returns>
        /// Author: LeDucTiep (11/06/2023)
        public static bool IsValid(object date)
        {
            if (date == null) return true;
            try
            {
                // Kiểm tra ngày hợp lệ 
                int dayOfYear = ((DateTime)date).DayOfYear;
                int year = ((DateTime)date).Year;
                if (year > DateTime.Now.Year)
                {
                    return false;
                }
                if(year == DateTime.Now.Year && dayOfYear > DateTime.Now.DayOfYear)
                    return false;

                return true;
            }
            catch { return true; }
        }
    }
}
