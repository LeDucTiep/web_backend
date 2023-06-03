using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher2023.Demo.DL.Entity;

namespace MISA.WebFresher2023.Demo.DL.Model
{
    public class EmployeePage
    {

        #region Contructor
        public EmployeePage() { }
        public EmployeePage(int totalRecord, IEnumerable<EmployeeOutPage> employees)
        {
            TotalRecord = totalRecord;
            Data = employees;
        }
        #endregion


        #region Method
        /// <summary>
        /// Tổng số bản ghi 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public int TotalRecord { get; set; }

        /// <summary>
        /// Danh sách nhân viên trả về
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public IEnumerable<EmployeeOutPage> Data { get; set; }
        #endregion
    }
}
