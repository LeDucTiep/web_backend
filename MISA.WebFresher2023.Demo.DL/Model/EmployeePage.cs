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
        public EmployeePage(int totalRecord, IEnumerable<EmployeeOutPage> employees)
        {
            TotalRecord = totalRecord;
            Data = employees;
        }
        public int TotalRecord { get; set; }
        public IEnumerable<EmployeeOutPage> Data { get; set; }
    }
}
