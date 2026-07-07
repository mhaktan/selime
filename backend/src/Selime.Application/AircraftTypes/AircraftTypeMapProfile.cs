using AutoMapper;
using Selime.Entities;
using Selime.AircraftTypes.Dto;

namespace Selime.AircraftTypes
{
    public class AircraftTypeMapProfile : Profile
    {
        public AircraftTypeMapProfile()
        {
            CreateMap<AircraftType, AircraftTypeDto>();
            CreateMap<CreateAircraftTypeDto, AircraftType>();
            CreateMap<AircraftTypeDto, AircraftType>();
        }
    }
}
