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
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        public BaseService(
            IBaseRepository<TEntity> baseRepository,
            IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public virtual async Task<int?> DeleteAsync(Guid id)
        {
            int? result = await _baseRepository.DeleteAsync(id);

            return result;
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

        public virtual async Task<int?> PostAsync(TEntityCreateDto entity)
        {
            TEntity ent = _mapper.Map<TEntity>(entity);

            int? result = await _baseRepository.PostAsync(ent);

            return result;
        }

        public virtual async Task<int?> UpdateAsync(Guid id, TEntityUpdateDto entity)
        {
            TEntity e = _mapper.Map<TEntity>(entity);

            int? result = await _baseRepository.UpdateAsync(id, e);

            return result;
        }
    }
}
