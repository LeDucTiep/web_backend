using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.Resource
{
    public class ResourceProcedure
    {
        public static readonly string EmployeeCheckExistCode = "Proc_Employee_CheckExistCode";
        public static readonly string PagingByFullNameOrEmployeeCode = "Proc_Employee_PagingByFullNameOrEmployeeCode";

        public static string GetAll(string tableName)
        {
            return $"Proc_{tableName}_GetAll";
        }
        public static string Update(string tableName)
        {
            return $"Proc_{tableName}_Edit";
        }
        public static string Add(string tableName)
        {
            return $"Proc_{tableName}_Add";
        }
        public static string Delete(string tableName)
        {
            return $"Proc_{tableName}_Delete";
        }
    }
}
