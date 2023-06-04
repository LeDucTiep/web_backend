using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Entity
{
    /// <summary>
    /// Class entity
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public abstract class BaseEntity
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// Created by: LeDucTiep (21/05/2023)
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// Created by: LeDucTiep (21/05/2023)
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// Created by: LeDucTiep (21/05/2023)
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        /// Created by: LeDucTiep (21/05/2023)
        public string? ModifiedBy { get; set; }
    }
}
