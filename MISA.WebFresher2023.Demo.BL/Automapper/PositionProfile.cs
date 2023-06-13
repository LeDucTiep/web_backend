using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;

namespace MISA.WebFresher2023.Demo.BL.Automapper
{
    /// <summary>
    /// Class position dành cho automapper 
    /// </summary>
    /// Author: LeDucTiep (07/06/2023)
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            // Map position sang positionDto
            CreateMap<Position, PositionDto>();
            // Map positionCreateDto sang Position
            CreateMap<PositionCreateDto, Position>();
            // Map positionUpdateDto sang Position
            CreateMap<PositionUpdateDto, Position>();

            CreateMap<PositionUpdateDto, Position>();

            CreateMap<PositionCreateDto, PositionDto>();

            CreateMap<PositionUpdateDto, PositionDto>();
        }
    }
}
