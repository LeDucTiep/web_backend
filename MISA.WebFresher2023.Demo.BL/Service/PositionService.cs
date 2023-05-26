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
        #region Contructor
        public PositionService(IPositionRepository positionRepository, IMapper mapper) : base(positionRepository, mapper)
        {
        }
        #endregion


        #region Method
        /// <summary>
        /// Hàm lấy tất cả chức vụ
        /// </summary>
        /// <returns>Danh sách chức vụ</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            // Tạo connection
            var connection = await _baseRepository.GetOpenConnectionAsync();

            // Tên procedure
            string procedure = "Proc_Position_GetAll";

            // Gọi đến procedure
            return await connection.QueryAsync<Position>(
                procedure,
                commandType: CommandType.StoredProcedure
            );
        }
        #endregion
    }
}
