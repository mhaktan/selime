using AutoMapper;
using Selime.Entities;
using Selime.Aircrafts.Dto;

namespace Selime.Aircrafts
{
    public class AircraftMapProfile : Profile
    {
        public AircraftMapProfile()
        {
            CreateMap<Aircraft, AircraftDto>();
            CreateMap<CreateAircraftDto, Aircraft>();
            CreateMap<AircraftDto, Aircraft>();
        }
    }
}
