using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.Resource;
using System.Net;

namespace MISA.WebFresher2023.Demo.Common.MyException
{
    /// <summary>
    /// Class lỗi từ nội bộ
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public class InternalException : BaseException
    {
        /// <summary>
        /// Http code
        /// </summary>
        /// Author: LeDucTiep (09/06/2023)
        public readonly int StatusCode = (int)HttpStatusCode.InternalServerError;

        #region Contructor
        public InternalException() : base()
        {
        }

        public InternalException(List<int> errorCode) : base(errorCode)
        {
            List<string> userMessage = base.UserMessage;
            List<string> devMessage = base.DevMessage;

            foreach (int code in errorCode)
            {
                switch (code)
                {
                    // Backend
                    case (int)InternalErrorCode.ConnectDbError:
                        userMessage.Add(InternalUserMessage.ConnectDbError);
                        devMessage.Add(InternalDevMessage.ConnectDbError);
                        break;
                }
            }

            ErrorCode = errorCode;
            UserMessage = userMessage;
            DevMessage = devMessage;
        } 
        #endregion
    }
}
