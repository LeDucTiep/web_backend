using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.BL.Service;

namespace MISA.WebFresher2023.Demo.Controllers
{
    [ApiController]
    public abstract class BaseController<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : ControllerBase
    {
        #region Field
        protected readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;
        #endregion

        #region Contructor
        public BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Method
        /// <summary>
        /// API Lấy một nhân viên theo id
        /// </summary>
        /// <param name="id">Id của nhân viên cần lấy</param>
        /// <returns>Thông tin của nhân viên đó</returns>
        /// Author: LeDucTiep (23/05/2023)
        // GET api/<EmployeeController>/guid
        [HttpGet("{id}")]
        public virtual async Task<TEntityDto?> GetAsync(Guid id)
        {
            var entityDto = await _baseService.GetAsync(id);
            return entityDto;
        }

        /// <summary>
        /// API xóa một bản ghi
        /// </summary>
        /// <param name="id">Mã của bản ghi cần xóa </param>
        /// <returns>Mã lỗi trả về</returns>
        /// Author: LeDucTiep (23/05/2023)
        [HttpDelete("{id}")]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _baseService.DeleteAsync(id);
        }

        /// <summary>
        /// API xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Mã của các bản ghi cần xóa </param>
        /// Author: LeDucTiep (23/05/2023)
        [HttpDelete]
        public virtual async Task DeleteManyAsync([FromBody] Guid[] arrayId)
        {
            await _baseService.DeleteManyAsync(arrayId);
        }

        /// <summary>
        /// API lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Author: LeDucTiep (23/05/2023)
        [HttpGet]
        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            return await _baseService.GetAllAsync();
        }
        #endregion
    }
}
