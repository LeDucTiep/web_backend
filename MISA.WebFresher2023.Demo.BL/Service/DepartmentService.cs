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
    public class DepartmentService : BaseService<Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>, IDepartmentService
    {
        #region Contructor
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : base(departmentRepository, mapper)
        {
        }
        #endregion

        #region Method
        /// <summary>
        /// Hàm lấy tất cả phòng ban
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            // Tạo connection
            var connection = await _baseRepository.GetOpenConnectionAsync();

            // Tên procedure
            string procedure = "Proc_Department_GetAll";

            // Gọi đến procedure
            return await connection.QueryAsync<Department>(
                procedure,
                commandType: CommandType.StoredProcedure
            );
        }
        #endregion
    }
}
