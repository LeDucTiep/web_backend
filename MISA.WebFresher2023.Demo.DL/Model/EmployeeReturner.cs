using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MISA.WebFresher2023.Demo.DL.Entity;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Model
{
    public class EmployeeReturner
    {
        public int? ErrorCode;
        public Employee Employee;

        public EmployeeReturner(int? errorCode, Employee employee)
        {
            ErrorCode = errorCode;
            Employee = employee;
        }
    }
}
