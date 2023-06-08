using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        /// <summary>
        /// Hàm thêm một bản ghi 
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns>TEntity</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task<TEntityDto> PostAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Lấy một bản ghi theo Id 
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task<TEntityDto?> GetAsync(Guid id);

        /// <summary>
        /// Sửa một bản ghi 
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="updateDto">Nội dung sửa</param>
        /// <returns></returns>
        /// Author: LeDucTiep (23/05/2023)
        Task UpdateAsync(Guid id, TEntityUpdateDto updateDto);

        /// <summary>
        /// Hàm xóa một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// <returns>Task</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="arrayId">Id của các bản ghi cần xóa</param>
        /// <returns>Task</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task DeleteManyAsync(Guid[] arrayId);

        /// <summary>
        /// Hàm lấy tất cả bản ghi 
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Author: LeDucTiep (23/05/2023)
        Task<IEnumerable<TEntityDto>> GetAllAsync();
    }
}
