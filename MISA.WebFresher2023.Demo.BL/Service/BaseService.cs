using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.Common;
using MISA.WebFresher2023.Demo.Common.Attribute;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.DL.Repository;
using System.ComponentModel.DataAnnotations;

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
        /// <returns>Số bản ghi đã xóa</returns>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            DeleteValidate(id);
            /// Xóa 
            return await _baseRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Xóa một bản ghi theo id 
        /// </summary>
        /// <param name="id">Id của bản ghi </param>
        /// <returns>Task</returns>
        /// <exception cref="NotFoundException">Lỗi không tìm thấy </exception>
        /// Author: LeDucTiep (23/05/2023)
        public virtual async Task<int> DeleteManyAsync(Guid[] arrayId)
        {
            /// Xóa và nhận về mã lỗi 
            return await _baseRepository.DeleteManyAsync(arrayId);
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
            TEntityDto entityDto = _mapper.Map<TEntityDto>(entity);

            Validate(entityDto);

            PostValidate(entityDto);

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
        public virtual async Task<int> UpdateAsync(Guid id, TEntityUpdateDto entity)
        {
            TEntityDto entityDto = _mapper.Map<TEntityDto>(entity);

            Validate(entityDto);

            UpdateValidate(id, entityDto);

            // Thêm trường id để trả về
            TEntity _entity = _mapper.Map<TEntity>(entity);

            // Update
            return await _baseRepository.UpdateAsync(id, _entity);
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

        /// <summary>
        /// Hàm validate
        /// </summary>
        /// Author: LeDucTiep (09/06/2023)
        public void Validate(TEntityDto entity)
        {
            System.Reflection.PropertyInfo[] properties = typeof(TEntityDto).GetProperties();

            List<int> errorCodes = new();

            foreach (System.Reflection.PropertyInfo property in properties)
            {
                var value = property.GetValue(entity, null);

                var attributeRequired = (MSRequiredAttribute?)property.GetCustomAttributes(typeof(MSRequiredAttribute), false).FirstOrDefault();

                if (attributeRequired != null && string.IsNullOrEmpty(value.ToString()))
                {
                    errorCodes.Add(attributeRequired.ErrorCode);
                }

                var attributeMaxLength = (MSMaxLengthAttribute?)property.GetCustomAttributes(typeof(MSMaxLengthAttribute), false).FirstOrDefault();

                if (attributeMaxLength != null && value != null && value.ToString().Length > attributeMaxLength.Length)
                {
                    errorCodes.Add(attributeMaxLength.ErrorCode);
                }
            }

            if (errorCodes.Count > 0)
            {
                throw new BadRequestException(errorCodes);
            }
        }

        /// <summary>
        /// Hàm validate theo từng loại service
        /// </summary>
        /// <typeparam name="T">Thực thể</typeparam>
        /// <param name="entity">Thực thể</param>
        /// Author: LeDucTiep (09/06/2023)
        public virtual void PostValidate(TEntityDto entity)
        {
        }

        /// <summary>
        /// Hàm validate theo từng loại service
        /// </summary>
        /// <typeparam name="T">Thực thể</typeparam>
        /// <param name="entity">Thực thể</param>
        /// <param name="id">Id của bản ghi</param>
        /// Author: LeDucTiep (09/06/2023)
        public virtual void UpdateValidate(Guid id, TEntityDto entity)
        {
        }

        /// <summary>
        /// Hàm validate theo từng loại service
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// Author: LeDucTiep (09/06/2023)
        public virtual void DeleteValidate(Guid id)
        {
        }

        #endregion
    }
}
