using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Automapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // Map employee sang employeeDto
            CreateMap<Employee, EmployeeDto>();
            // Map employeeCreateDto sang employee
            CreateMap<EmployeeCreateDto, Employee>();
            // Map employeeUpdateDto sang employee
            CreateMap<EmployeeUpdateDto, Employee>();
        }
    }
}
