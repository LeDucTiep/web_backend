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
        public async Task<IEnumerable<PositionDto>> GetAllAsync()
        {
            // Gọi đến procedure
            IEnumerable<Position> myList = await _baseRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<PositionDto>>(myList);
        }
        #endregion
    }
}
