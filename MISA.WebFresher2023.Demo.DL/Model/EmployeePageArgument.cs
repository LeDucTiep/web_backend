using MISA.WebFresher2023.Demo.Common.Attribute;
using MISA.WebFresher2023.Demo.Common.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Model
{
    public class EmployeePageArgument
    {
        private int pageSize = 20;
        private int pageNumber = 1;
        private string employeeSearchTerm = "";

        /// <summary>
        /// Kích thước trang 
        /// </summary>
        /// Author: LeDucTiep (09/06/2023)
        public int PageSize { get => pageSize; set => pageSize = value; }

        /// <summary>
        /// Số trang
        /// </summary>
        /// Author: LeDucTiep (09/06/2023)
        public int PageNumber { get => pageNumber; set => pageNumber = value; }

        /// <summary>
        /// Từ khóa cần tìm kiếm
        /// </summary>
        /// Author: LeDucTiep (09/06/2023)
        [MSMaxLength(Length = 255, ErrorCode = (int)PagingErrorCode.EmployeeSearchTermTooLong)]
        public string EmployeeSearchTerm { get => employeeSearchTerm; set => employeeSearchTerm = value; }
    }
}
