using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.Constant
{
    public enum ErrorCodeConst
    {
         EmployeeIdNotFound = 1001,
         EmployeeCodeDuplicated = 1002,

         DepartmentIdNotFound = 1003,

         PositionIdNotFound = 1004,

         PagingInvalidPageSize = 1005,
         PagingInvalidPageNumber = 1006,
    }
}
