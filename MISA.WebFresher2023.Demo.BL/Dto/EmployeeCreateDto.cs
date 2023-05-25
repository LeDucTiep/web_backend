using MISA.WebFresher2023.Demo.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Dto
{
    public class EmployeeCreateDto
    {
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IdentityNumber { get; set; }
        public DateTime? IdentityDate { get; set; }
        public string? IdentityPlace { get; set; }
        public Guid? PositionId { get; set; }
        public Guid DepartmentId { get; set; }
    }
}
