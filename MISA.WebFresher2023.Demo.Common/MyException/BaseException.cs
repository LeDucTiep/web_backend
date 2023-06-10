using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.Resource;
using System.Text.Json;

namespace MISA.WebFresher2023.Demo.Common.MyException
{
    /// <summary>
    /// Class Exception
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public class BaseException : Exception
    {
        #region Field
        /// <summary>
        /// Mã lỗi
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public List<int> ErrorCode { get; set; }

        /// <summary>
        /// Thông báo cho người dùng
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public List<string> UserMessage { get; set; }

        /// <summary>
        /// Thông báo cho dev 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public List<string> DevMessage { get; set; }

        /// <summary>
        /// Id để truy vết 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public string TraceId { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public string MoreInfo { get; set; }
        #endregion

        #region Contructor
        public BaseException() : base()
        {

            ErrorCode = new List<int>() { (int)InternalErrorCode.Unknown };
            UserMessage = new List<string>() { InternalUserMessage.Unknown };
            DevMessage = new List<string>() { base.Message };
            MoreInfo = base.HelpLink ?? "";
        }
        public BaseException(List<int> errorCode) : base()
        {
            ErrorCode = errorCode;
            UserMessage = new();
            DevMessage = new();
            MoreInfo = base.HelpLink ?? "";
        }
        #endregion

        #region Method
        /// <summary>
        /// Chuyển sang dạng json
        /// </summary>
        /// <returns>Chuỗi json</returns>
        /// Author: LeDucTiep (23/05/2023)
        public override string ToString()
        {
            return JsonSerializer.Serialize(
                    new
                    {
                        ErrorCode = ErrorCode.ToArray(),
                        UserMessage = UserMessage.ToArray(),
                        DevMessage = DevMessage.ToArray(),
                        TraceId,
                        MoreInfo
                    }
                );
        }
        #endregion
    }
}
