using MISA.WebFresher2023.Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Repository
{
    public interface IBaseRepository <TEntity> 
    {
        /// <summary>
        /// Hàm mở kết nối với database
        /// </summary>
        /// <returns>DbConnection</returns>
        /// Created by: LeDucTiep (21/05/2023)
        Task<DbConnection> GetOpenConnectionAsync();
        /// <summary>
        /// Hàm lấy một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>TEntity</returns>
        /// Created by: LeDucTiep (21/05/2023)
        Task<TEntity?> GetAsync(Guid id);
        /// <summary>
        /// Hàm cập nhật một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entity">Giá trị của bản ghi</param>
        /// <returns>TEntity</returns>
        /// Created by: LeDucTiep (21/05/2023)
        Task<TEntity?> UpdateAsync(Guid id, TEntity entity);
        /// <summary>
        /// Hàm xóa một bàn ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Tổng số bản ghi được xóa</returns>
        /// Created by: LeDucTiep (21/05/2023)
        Task<int> DeleteAsync(Guid id);

    }
}
