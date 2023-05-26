using MISA.WebFresher2023.Demo.Common.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.MyException
{
    public class ExsistedException : Exception
    {
        #region Field 
        /// <summary>
        /// Mã lỗi nội bộ
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public int ErrorCode { get; set; }
        #endregion

        #region Contructor
        public ExsistedException() { }

        public ExsistedException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public ExsistedException(string? message) : base(message)
        {
        }

        public ExsistedException(string? message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}
