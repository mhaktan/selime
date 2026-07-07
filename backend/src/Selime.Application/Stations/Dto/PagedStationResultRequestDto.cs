using System;
using Abp.Application.Services.Dto;

namespace Selime.Stations.Dto
{
    public class PagedStationResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
}
