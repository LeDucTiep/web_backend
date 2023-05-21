using AutoMapper;
using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.DL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public class BaseService<TEntity, TEntityDto, TEntityUpdateDto> : IBaseService<TEntityDto, TEntityUpdateDto>
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

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);

            if(entity == null)
            {
                throw new NotFoundException("Khong tim thay ban ghi", errorCode: ErrorCodeConst.BusinessNotFound);
            }

           var result = await _baseRepository.DeleteAsync(id);

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

        public virtual Task<TEntityDto?> UpdateAsync(Guid id, TEntityUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
