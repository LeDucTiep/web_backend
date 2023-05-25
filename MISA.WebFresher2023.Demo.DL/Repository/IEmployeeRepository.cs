using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;

namespace MISA.WebFresher2023.Demo.DL.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        public Task<bool> CheckEmployeeCode(string employeeCode);
        public Task<string> GetNewEmployeeCode();
        public Task<EmployeePage> GetPage(int pageSize, int pageNumber, string? employeeFilter);
        public new Task<int> PostAsync(Employee employee);
    }
}
