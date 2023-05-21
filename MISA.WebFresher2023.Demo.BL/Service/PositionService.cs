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
    public class PositionService : BaseService<Department, DepartmentDto, DepartmentUpdateDto>, IPositionService
    {
        public PositionService(IBaseRepository<Department> baseRepository, IMapper mapper) : base(baseRepository, mapper)
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

        public Task<PositionDto?> UpdateAsync(Guid id, PositionUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }

        Task<PositionDto?> IBaseService<PositionDto, PositionUpdateDto>.GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
