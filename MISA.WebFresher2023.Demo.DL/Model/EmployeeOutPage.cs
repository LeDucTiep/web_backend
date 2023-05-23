using MISA.WebFresher2023.Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Model
{
    public class EmployeeOutPage : Employee
    {
        public string? PositionName { get; set; }
        public string? DepartmentName { get; set; }
    }
}
