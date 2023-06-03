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
        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            // Gọi đến procedure
            IEnumerable<Department> myList = await _baseRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<DepartmentDto>>(myList);
        }
        #endregion
    }
}
