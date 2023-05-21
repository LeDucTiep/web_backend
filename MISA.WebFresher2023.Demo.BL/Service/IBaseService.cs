using MISA.WebFresher2023.Demo.BL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public interface IBaseService<TEntityDto, TEntityUpdateDto>
    {
        Task<TEntityDto?> GetAsync(Guid id);
        Task<TEntityDto?> UpdateAsync(Guid id, TEntityUpdateDto updateDto);
        Task<int> DeleteAsync(Guid id);
    }
}
