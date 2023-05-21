using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Entity
{
    public class EmployeePageDto
    {
        public EmployeePageDto(int totalRecord, IEnumerable<Employee> employees)
        {
            TotalRecord = totalRecord;
            Data = employees;
        }
        public int TotalRecord { get; set; }
        public IEnumerable<Employee> Data { get; set; }
    }
}
