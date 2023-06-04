using MISA.WebFresher2023.Demo.Common.Constant;

namespace MISA.WebFresher2023.Demo.Common.MyException
{
    /// <summary>
    /// Class tham số của api phân trang
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public class PagingArgumentException : Exception
    {
        #region Field
        /// <summary>
        /// Mã lỗi nội bộ
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public int ErrorCode { get; set; }
        #endregion

        #region Contructor
        public PagingArgumentException() { }

        public PagingArgumentException(int errorCode)
        {
            ErrorCode = errorCode;

        }

        public PagingArgumentException(string? message) : base(message)
        {
        }

        public PagingArgumentException(string? message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}
