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
    public class PositionController : BaseController<Position, PositionDto, PositionCreateDto, PositionUpdateDto>
    {
        #region Field
        /// <summary>
        /// PositionService
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        private readonly IPositionService _positionService;
        #endregion

        #region Contructor
        public PositionController(IPositionService positionService
            ) : base(positionService)
        {
            _positionService = positionService;
        }
        #endregion

        #region Method
        /// <summary>
        /// API lấy tất cả chức vụ
        /// </summary>
        /// <returns>Danh sách chức vụ</returns>
        /// Author: LeDucTiep (23/05/2023)
        // GET api/<PositionController>/5
        [HttpGet]
        public async Task<IEnumerable<PositionDto>> GetAllAsync()
        {
            return await _positionService.GetAllAsync();
        }
        #endregion
    }
}
