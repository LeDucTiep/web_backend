using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.Common;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.DL.Repository;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    /// <summary>
    /// Class dịch vụ cơ bản
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TEntityCreateDto"></typeparam>
    /// <typeparam name="TEntityUpdateDto"></typeparam>
    /// Author: LeDucTiep (23/05/2023)
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
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task DeleteAsync(Guid id)
        {
            /// Xóa 
            await _baseRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Xóa một bản ghi theo id 
        /// </summary>
        /// <param name="id">Id của bản ghi </param>
        /// <returns>Task</returns>
        /// <exception cref="NotFoundException">Lỗi không tìm thấy </exception>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task DeleteManyAsync(Guid[] arrayId)
        {
            /// Xóa và nhận về mã lỗi 
            await _baseRepository.DeleteManyAsync(arrayId);
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
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task<TEntityDto> PostAsync(TEntityCreateDto entity)
        {
            TEntity entity1 = _mapper.Map<TEntity>(entity);

            await _baseRepository.PostAsync(entity1);

            TEntityDto entity2 = _mapper.Map<TEntityDto>(entity1);

            return entity2;
        }

        /// <summary>
        /// Hàm update một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entity">Giá trị bản ghi</param>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task UpdateAsync(Guid id, TEntityUpdateDto entity)
        {
            // Thêm trường id để trả về
            TEntity _entity = _mapper.Map<TEntity>(entity);

            // Update
            await _baseRepository.UpdateAsync(id, _entity);
        }

        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            // Gọi đến procedure
            IEnumerable<TEntity> myList = await _baseRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<TEntityDto>>(myList);
        }
        #endregion
    }
}
