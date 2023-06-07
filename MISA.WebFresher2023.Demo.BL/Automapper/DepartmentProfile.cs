using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;

namespace MISA.WebFresher2023.Demo.BL.Automapper
{
    /// <summary>
    /// Class department dành cho automapper 
    /// </summary>
    /// Author: LeDucTiep (07/06/2023)
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            // Map department sang departmentDto
            CreateMap<Department, DepartmentDto>();
            // Map departmentCreateDto sang Department
            CreateMap<DepartmentCreateDto, Department>();
            /// Map departmentUpdateDto sang department
            CreateMap<DepartmentUpdateDto, Department>();
        }
    }
}
