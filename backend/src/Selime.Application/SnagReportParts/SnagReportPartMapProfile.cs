using AutoMapper;
using Selime.Entities;
using Selime.SnagReportParts.Dto;

namespace Selime.SnagReportParts
{
    public class SnagReportPartMapProfile : Profile
    {
        public SnagReportPartMapProfile()
        {
            CreateMap<SnagReportPart, SnagReportPartDto>();
            CreateMap<CreateSnagReportPartDto, SnagReportPart>();
            CreateMap<SnagReportPartDto, SnagReportPart>();
        }
    }
}
