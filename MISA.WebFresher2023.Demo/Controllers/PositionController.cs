using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.BL.Service;
using MISA.WebFresher2023.Demo.DL.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher2023.Demo.Controllers
{
    [Route("api/v1/[controller]s")]
    public class PositionController : BaseController<Position, PositionDto, PositionCreateDto, PositionUpdateDto>
    {
        public PositionController(IPositionService positionService
            ) : base(positionService)
        {
            /// <summary>
            /// API lấy tất cả danh sách bản ghi
            /// </summary>
            /// <returns>Danh sách bản ghi</returns>
            /// Author: LeDucTiep (23/05/2023)
        }
        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<PositionDto> list = await _baseService.GetAllAsync();

            if (!list.Any())
                return NoContent();

            return Ok(list);
        }
    }
}
