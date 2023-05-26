using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Automapper
{
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
        }
    }
}
