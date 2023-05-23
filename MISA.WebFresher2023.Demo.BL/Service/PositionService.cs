using AutoMapper;
using Dapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Repository;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public class PositionService : BaseService<Position, PositionDto, PositionCreateDto, PositionUpdateDto>, IPositionService
    {
        public PositionService(IPositionRepository positionRepository, IMapper mapper) : base(positionRepository, mapper)
        {
        }
        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            // Tạo connection
            var connection = await _baseRepository.GetOpenConnectionAsync();

            // Gọi đến procedure
            return await connection.QueryAsync<Position>(
                "Proc_Position_GetAll",
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
