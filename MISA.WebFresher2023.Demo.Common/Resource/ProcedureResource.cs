﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.Resource
{
    /// <summary>
    /// Class tên procedure
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
    public class ProcedureResource
    {
        #region Field
        /// <summary>
        /// Procedure kiểm cha employeeCode đã tồn tại chưa 
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string CheckDuplicatedCode = "Proc_Employee_CheckDuplicatedCode";

        /// <summary>
        /// Procedure kiểm cha employeeCode muốn sửa đã tồn tại chưa, ngoại trừ mã trước khi sửa
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string CheckDuplicatedCodeExceptItsCode = "Proc_Employee_CheckDuplicatedCodeExceptItsCode";

        /// <summary>
        /// Procedure kiểm cha employeeCode muốn sửa đã tồn tại chưa, ngoại trừ mã trước khi sửa
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string CheckDuplicatedCodeExceptItsId = "Proc_Employee_CheckDuplicatedCodeExceptItsId";

        /// <summary>
        /// Procedure phân trang theo họ tên và mã nhân viên
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string Paging = "Proc_Employee_Paging";

        /// <summary>
        /// Procedure phân trang theo họ tên và mã nhân viên
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        public static readonly string EmployeeExport = "Proc_Employee_Export";
        #endregion

        #region Method
        /// <summary>
        /// Procedure Kiểm tra bản ghi có tồn tại không bằng id bản ghi
        /// </summary>
        /// <param name="tableName">Tên của bảng</param>
        /// <returns>Procedure</returns>
        /// Author: LeDucTiep (23/05/2023)
        public static string CheckExistedById(string tableName)
        {
            return $"Proc_{tableName}_CheckExistedById";
        }

        /// <summary>
        /// Procedure lấy tất cả bản ghi
        /// </summary>
        /// <param name="tableName">Tên của bảng</param>
        /// <returns>Procedure</returns>
        /// Author: LeDucTiep (23/05/2023)
        public static string GetAll(string tableName)
        {
            return $"Proc_{tableName}_GetAll";
        }

        /// <summary>
        /// Procedure lấy một bản ghi
        /// </summary>
        /// <param name="tableName">Tên của bảng</param>
        /// <returns>Procedure</returns>
        /// Author: LeDucTiep (23/05/2023)
        public static string Get(string tableName)
        {
            return $"Proc_{tableName}_GetById";
        }

        /// <summary>
        /// Procedure cập nhật bản ghi
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <returns>Procedure</returns>
        /// Author: LeDucTiep (23/05/2023)
        public static string Update(string tableName)
        {
            return $"Proc_{tableName}_Edit";
        }

        /// <summary>
        /// Procedure thêm một bản ghi
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <returns>Procedure</returns>
        /// Author: LeDucTiep (23/05/2023)
        public static string Add(string tableName)
        {
            return $"Proc_{tableName}_Add";
        }

        /// <summary>
        /// Procedure xóa một bản ghi
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <returns>Procedure</returns>
        /// Author: LeDucTiep (23/05/2023)
        public static string Delete(string tableName)
        {
            return $"Proc_{tableName}_Delete";
        }

        /// <summary>
        /// Procedure xóa nhiều bản ghi
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <returns>Procedure</returns>
        /// Author: LeDucTiep (23/05/2023)
        public static string DeleteMany(string tableName)
        {
            return $"Proc_{tableName}_DeleteMany";
        }
        #endregion
    }
}
