using AutoMapper;
using Selime.Entities;
using Selime.Stations.Dto;

namespace Selime.Stations
{
    public class StationMapProfile : Profile
    {
        public StationMapProfile()
        {
            CreateMap<Station, StationDto>();
            CreateMap<CreateStationDto, Station>();
            CreateMap<StationDto, Station>();
        }
    }
}
