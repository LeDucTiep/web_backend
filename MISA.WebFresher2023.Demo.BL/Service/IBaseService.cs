using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public interface IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        Task<int?> PostAsync(TEntityCreateDto employee);
        Task<TEntityDto?> GetAsync(Guid id);
        Task<int?> UpdateAsync(Guid id, TEntityUpdateDto updateDto);
        Task<int?> DeleteAsync(Guid id);
    }
}
