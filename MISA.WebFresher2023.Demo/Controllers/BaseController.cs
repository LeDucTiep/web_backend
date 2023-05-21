using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.BL.Service;

namespace MISA.WebFresher2023.Demo.Controllers
{
    [ApiController]
    public abstract class BaseController <TEntityDto, TEntityUpdateDto>: ControllerBase
    {
        protected readonly IBaseService<TEntityDto, TEntityUpdateDto> _baseService;

        public BaseController(IBaseService<TEntityDto, TEntityUpdateDto> baseService)
        {
            _baseService = baseService;
        }

        /// <summary>
        /// API Lấy một nhân viên theo id
        /// </summary>
        /// <param name="id">Id của nhân viên cần lấy</param>
        /// <returns>Thông tin của nhân viên đó</returns>
        // GET api/<EmployeeController>/guid
        [HttpGet("{id}")]
        public virtual async Task<TEntityDto?> GetAsync(Guid id)
        {
            var entityDto = await _baseService.GetAsync(id);
            return entityDto;
        }
        [HttpDelete("{id}")]
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var result = await _baseService.DeleteAsync(id);
            return result;
        }

    }
}
