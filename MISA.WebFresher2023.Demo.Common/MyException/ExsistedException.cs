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
        public ErrorCodeConst ErrorCode { get; set; }
        public ExsistedException() { }
        public ExsistedException(ErrorCodeConst errorCode)
        {
            ErrorCode = errorCode;

        }
        public ExsistedException(string? message) : base(message)
        {
        }
        public ExsistedException(string? message, ErrorCodeConst errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
