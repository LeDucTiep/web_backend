using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;

namespace MISA.WebFresher2023.Demo.BL.Automapper
{
    /// <summary>
    /// Class employee dành cho automapper 
    /// </summary>
    /// Author: LeDucTiep (07/06/2023)
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

            CreateMap<EmployeeUpdateDto, Employee>();

            CreateMap<EmployeeCreateDto, EmployeeDto>();

            CreateMap<EmployeeUpdateDto, EmployeeDto>();
            
        }
    }
}
