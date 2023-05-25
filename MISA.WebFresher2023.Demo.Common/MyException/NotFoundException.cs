using MISA.WebFresher2023.Demo.Common.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.MyException
{
    public class NotFoundException : Exception
    {
        public ErrorCodeConst ErrorCode { get; set; }
        public NotFoundException() { }
        public NotFoundException(ErrorCodeConst errorCode) { 
            ErrorCode = errorCode;
        
        }
        public NotFoundException(string? message) : base(message)
        {
        }
        public NotFoundException(string? message, ErrorCodeConst errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

    }
}
