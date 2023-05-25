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
    public class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        public BaseService(
            IBaseRepository<TEntity> baseRepository,
            IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            int result = await _baseRepository.DeleteAsync(id);

            if (result != 0)
                throw new NotFoundException("BaseService.DeleteAsync", (ErrorCodeConst)result);
        }

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

        public virtual async Task PostAsync(TEntityCreateDto entity)
        {
            TEntity ent = _mapper.Map<TEntity>(entity);

            int errorCode = await _baseRepository.PostAsync(ent);

            if (errorCode != 0)
                throw new NotFoundException("EmployeeService.PostAsync", (ErrorCodeConst)errorCode);
            else if (errorCode == 1002)
                throw new ExsistedException("EmployeeService.PostAsync", (ErrorCodeConst)errorCode);
        }
        /// <summary>
        /// Hàm update một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entity">Giá trị bản ghi</param>
        /// <returns>Mã lỗi</returns>
        public virtual async Task UpdateAsync(Guid id, TEntityUpdateDto entity)
        {
            TEntity e = _mapper.Map<TEntity>(entity);

            int result = await _baseRepository.UpdateAsync(id, e);

            if (result == 1002)
            {
                throw new ExsistedException("BaseService.UpdateAsync", (ErrorCodeConst)result);
            }
            else
            {
                throw new NotFoundException("BaseService.UpdateAsync", (ErrorCodeConst)result);
            }
        }
    }
}
