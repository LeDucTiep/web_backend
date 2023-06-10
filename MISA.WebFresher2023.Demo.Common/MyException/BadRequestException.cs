using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.Resource;
using System.Net;

namespace MISA.WebFresher2023.Demo.Common.MyException
{
    /// <summary>
    /// Class Exception
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public class BadRequestException : BaseException
    {
        #region Field 
        /// <summary>
        /// Http code
        /// </summary>
        /// Author: LeDucTiep (09/06/2023)
        public readonly int StatusCode = (int)HttpStatusCode.BadRequest;
        #endregion

        #region Contructor
        public BadRequestException() : base()
        {
        }

        public BadRequestException(List<int> errorCode) : base(errorCode)
        {
            List<string> userMessage = base.UserMessage;
            List<string> devMessage = base.DevMessage;

            foreach (int code in errorCode)
            {
                switch (code)
                {
                    // Nhân viên 
                    case (int)EmployeeErrorCode.CodeDuplicated:
                        userMessage.Add(EmployeeUserMessage.CodeDuplicated);
                        devMessage.Add(EmployeeUserMessage.CodeDuplicated);
                        break;

                    case (int)EmployeeErrorCode.IdNotFound:
                        userMessage.Add(EmployeeUserMessage.IdNotFound);
                        devMessage.Add(EmployeeDevMessage.IdNotFound);
                        break;

                    case (int)EmployeeErrorCode.CodeIsRequired:
                        userMessage.Add(EmployeeUserMessage.CodeIsRequired);
                        devMessage.Add(EmployeeDevMessage.CodeIsRequired);
                        break;

                    case (int)EmployeeErrorCode.FullNameIsRequired:
                        userMessage.Add(EmployeeUserMessage.FullNameIsRequired);
                        devMessage.Add(EmployeeDevMessage.FullNameIsRequired);
                        break;

                    case (int)EmployeeErrorCode.EmployeeCodeTooLong:
                        userMessage.Add(EmployeeUserMessage.EmployeeCodeTooLong);
                        devMessage.Add(EmployeeDevMessage.EmployeeCodeTooLong);
                        break;

                    case (int)EmployeeErrorCode.FullNameTooLong:
                        userMessage.Add(EmployeeUserMessage.FullNameTooLong);
                        devMessage.Add(EmployeeDevMessage.FullNameTooLong);
                        break;

                    case (int)EmployeeErrorCode.EmailTooLong:
                        userMessage.Add(EmployeeUserMessage.EmailTooLong);
                        devMessage.Add(EmployeeDevMessage.EmailTooLong);

                        break;
                    case (int)EmployeeErrorCode.AddressTooLong:
                        userMessage.Add(EmployeeUserMessage.AddressTooLong);
                        devMessage.Add(EmployeeDevMessage.AddressTooLong);
                        break;

                    case (int)EmployeeErrorCode.PhoneNumberTooLong:
                        userMessage.Add(EmployeeUserMessage.PhoneNumberTooLong);
                        devMessage.Add(EmployeeDevMessage.PhoneNumberTooLong);
                        break;

                    case (int)EmployeeErrorCode.IdentityNumberTooLong:
                        userMessage.Add(EmployeeUserMessage.IdentityNumberTooLong);
                        devMessage.Add(EmployeeDevMessage.IdentityNumberTooLong);

                        break;
                    case (int)EmployeeErrorCode.IdentityPlaceTooLong:
                        userMessage.Add(EmployeeUserMessage.IdentityPlaceTooLong);
                        devMessage.Add(EmployeeDevMessage.IdentityPlaceTooLong);
                        break;

                    case (int)EmployeeErrorCode.BankAccountNumberTooLong:
                        userMessage.Add(EmployeeUserMessage.BankAccountNumberTooLong);
                        devMessage.Add(EmployeeDevMessage.BankAccountNumberTooLong);
                        break;

                    case (int)EmployeeErrorCode.NameOfBankTooLong:
                        userMessage.Add(EmployeeUserMessage.NameOfBankTooLong);
                        devMessage.Add(EmployeeDevMessage.NameOfBankTooLong);
                        break;

                    case (int)EmployeeErrorCode.BankAccountBranchTooLong:
                        userMessage.Add(EmployeeUserMessage.BankAccountBranchTooLong);
                        devMessage.Add(EmployeeDevMessage.BankAccountBranchTooLong);
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

                    // Phân trang
                    case (int)PagingErrorCode.InvalidPageSize:
                        userMessage.Add(PagingUserMessage.InvalidPageSize);
                        devMessage.Add(PagingDevMessage.InvalidPageSize);
                        break;

                    case (int)PagingErrorCode.InvalidPageNumber:
                        userMessage.Add(PagingUserMessage.InvalidPageNumber);
                        devMessage.Add(PagingDevMessage.InvalidPageNumber);
                        break;

                    case (int)PagingErrorCode.EmployeeSearchTermTooLong:
                        userMessage.Add(PagingUserMessage.EmployeeSearchTermTooLong);
                        devMessage.Add(PagingDevMessage.EmployeeSearchTermTooLong);
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
