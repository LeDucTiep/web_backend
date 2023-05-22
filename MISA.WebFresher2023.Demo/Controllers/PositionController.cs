using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.BL.Service;
using MISA.WebFresher2023.Demo.DL.Entity;
using MySqlConnector;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher2023.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : BaseController<Position, PositionDto, PositionCreateDto, PositionUpdateDto>
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService
            ) : base(positionService)
        {
            _positionService = positionService;
        }

        // GET api/<PositionController>/5
        [HttpGet]
        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            return await _positionService.GetAllAsync();
        }
    }
}
