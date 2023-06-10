using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.MyException
{
    /// <summary>
    /// Class lỗi không tìm thấy
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public class NotFoundException : BaseException
    {
        #region Field
        /// <summary>
        /// Http code
        /// </summary>
        /// Author: LeDucTiep (09/06/2023)
        public readonly int StatusCode = (int)HttpStatusCode.NotFound;
        #endregion

        #region Contructor
        public NotFoundException() : base()
        {
        }

        public NotFoundException(List<int> errorCode) : base(errorCode)
        {
            List<string> userMessage = base.UserMessage;
            List<string> devMessage = base.DevMessage;

            foreach (int code in errorCode)
            {
                switch (code)
                {
                    // Nhân viên 
                    case (int)EmployeeErrorCode.IdNotFound:
                        userMessage.Add(EmployeeUserMessage.IdNotFound);
                        devMessage.Add(EmployeeDevMessage.IdNotFound);
                        break;

                    // Phòng ban 
                    case (int)DepartmentErrorCode.IdNotFound:
                        userMessage.Add(DepartmentUserMessage.IdNotFound);
                        devMessage.Add(DepartmentDevMessage.IdNotFound);
                        break;

                    // Chức vụ
                    case (int)PositionErrorCode.IdNotFound:
                        userMessage.Add(PositionUserMessage.IdNotFound);
                        devMessage.Add(PositionDevMessage.IdNotFound);
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
