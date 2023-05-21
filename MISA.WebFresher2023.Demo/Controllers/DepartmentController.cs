using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.BL.Service;
using MISA.WebFresher2023.Demo.DL.Entity;
using MySqlConnector;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher2023.Demo.Controllers
{
    [Route("api/v1/[controller]s")]
    public class DepartmentController : BaseController<DepartmentDto, DepartmentUpdateDto>
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService) : base(departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<IEnumerable<Department>> Get()
        {
            return await _departmentService.GetAllAsync();
        }
    }
}
