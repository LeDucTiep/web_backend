using AutoMapper;
using MISA.WebFresher2023.Demo.Common;
using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.Common.Resource;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
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
            ProcessErrorCode.process(errorCode);
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
        public virtual async Task<TEntityDto> PostAsync(TEntityCreateDto entity)
        {
            TEntity entity1 = _mapper.Map<TEntity>(entity);

            int errorCode = await _baseRepository.PostAsync(entity1);

            ProcessErrorCode.process(errorCode);

            TEntityDto entity2 = _mapper.Map<TEntityDto>(entity1);

            return entity2;
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

            int errorCode = await _baseRepository.UpdateAsync(id, _entity);

            // Trùng mã nhân viên

            ProcessErrorCode.process(errorCode);
        }
        #endregion
    }
}
