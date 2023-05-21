using MISA.WebFresher2023.Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<bool> CheckEmployeeCode(string employeeCode);
    }
}
