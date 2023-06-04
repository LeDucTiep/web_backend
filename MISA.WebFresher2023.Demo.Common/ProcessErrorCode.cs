using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.Common.Resource;

namespace MISA.WebFresher2023.Demo.Common
{
    /// <summary>
    /// Class xử lý mã lỗi
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public static class ProcessErrorCode
    {
        public static void process(int errorCode)
        {
            switch (errorCode)
            {
                case (int)EmployeeErrorCode.CodeDuplicated:
                    // Trùng mã nhân viên 
                    throw new ExsistedException(EmployeeErrorMessage.CodeDuplicated, errorCode);

                case (int)DepartmentErrorCode.IdNotFound:
                    // Không tìm thấy Id phòng ban 
                    throw new NotFoundException(DepartmentErrorMessage.IdNotFound, errorCode);

                case (int)PositionErrorCode.IdNotFound:
                    // Không tìm thấy Id chức vụ
                    throw new NotFoundException(PositionErrorMessage.IdNotFound, errorCode);

                case (int)EmployeeErrorCode.IdNotFound:
                    // Không tìm thấy Id nhân viên
                    throw new NotFoundException(EmployeeErrorMessage.IdNotFound, errorCode);
            }
        }
    }
}
