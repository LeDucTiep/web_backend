using AutoMapper;
using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        #region Field
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        #endregion

        #region Contructor 
        public BaseService(
            IBaseRepository<TEntity> baseRepository,
            IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        #endregion

        #region Method

        /// <summary>
        /// Xóa một bản ghi theo id 
        /// </summary>
        /// <param name="id">Id của bản ghi </param>
        /// <returns>Task</returns>
        /// <exception cref="NotFoundException">Lỗi không tìm thấy </exception>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task DeleteAsync(Guid id)
        {
            /// Xóa và nhận về mã lỗi 
            int errorCode = await _baseRepository.DeleteAsync(id);
            /// nếu có lỗi xảy ra thì ném lỗi 
            if (errorCode != 0)
                throw new NotFoundException("BaseService.DeleteAsync", errorCode);
        }

        /// <summary>
        /// Hàm lấy một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần lấy </param>
        /// <returns>Task<TEmployeeId></returns>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task<TEntityDto?> GetAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);

            if (entity == null)
            {
                return default;
            }

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }

        /// <summary>
        /// Thêm một bản ghi
        /// </summary>
        /// <param name="entity">Loại bản ghi </param>
        /// <returns>TEntity</returns>
        /// <exception cref="NotFoundException">Lỗi không tìm thấy</exception>
        /// <exception cref="ExsistedException">Lỗi đã tồn tại</exception>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task<TEntity> PostAsync(TEntityCreateDto entity)
        {
            TEntity ent = _mapper.Map<TEntity>(entity);

            int errorCode = await _baseRepository.PostAsync(ent);

            if (errorCode != 0)
                throw new NotFoundException("BaseService.PostAsync", errorCode);
            else if (errorCode.Equals(EmployeeErrorCode.CodeDuplicated))
                throw new ExsistedException("BaseService.PostAsync", errorCode);

            return ent;
        }
        /// <summary>
        /// Hàm update một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entity">Giá trị bản ghi</param>
        /// <returns>Mã lỗi</returns>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task UpdateAsync(Guid id, TEntityUpdateDto entity)
        {
            TEntity _entity = _mapper.Map<TEntity>(entity);

            int result = await _baseRepository.UpdateAsync(id, _entity);

            // Trùng mã nhân viên
            if (result.Equals(EmployeeErrorCode.CodeDuplicated))
            {
                throw new ExsistedException("BaseService.UpdateAsync", result);
            }

            else if (
                // Không tìm thấy Id phòng ban 
                result.Equals(DepartmentErrorCode.IdNotFound) ||
                // Không tìm thấy Id chức vụ
                result.Equals(PositionErrorCode.IdNotFound) ||
                // Không tìm thấy Id nhân viên
                result.Equals(EmployeeErrorCode.IdNotFound))
            {
                throw new NotFoundException("BaseService.UpdateAsync", result);
            }
        }
        #endregion
    }
}
