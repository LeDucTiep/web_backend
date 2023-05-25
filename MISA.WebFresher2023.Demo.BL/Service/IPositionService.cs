using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public interface IPositionService : IBaseService<PositionDto, PositionCreateDto, PositionUpdateDto>
    {
        public Task<IEnumerable<Position>> GetAllAsync();
    }
}
