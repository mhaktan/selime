using AutoMapper;
using Selime.Entities;
using Selime.SnagReports.Dto;

namespace Selime.SnagReports
{
    public class SnagReportMapProfile : Profile
    {
        public SnagReportMapProfile()
        {
            CreateMap<SnagReport, SnagReportDto>()
                .ForMember(d => d.StatusName, o => o.MapFrom(s => s.Status.ToString()));
            CreateMap<CreateSnagReportDto, SnagReport>();
            CreateMap<SnagReportDto, SnagReport>();
        }
    }
}
