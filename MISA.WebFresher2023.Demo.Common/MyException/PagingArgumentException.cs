using MISA.WebFresher2023.Demo.Common.Constant;

namespace MISA.WebFresher2023.Demo.Common.MyException
{
    public class PagingArgumentException : Exception
    {
        public ErrorCodeConst ErrorCode { get; set; }
        public PagingArgumentException() { }
        public PagingArgumentException(ErrorCodeConst errorCode)
        {
            ErrorCode = errorCode;

        }
        public PagingArgumentException(string? message) : base(message)
        {
        }
        public PagingArgumentException(string? message, ErrorCodeConst errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
