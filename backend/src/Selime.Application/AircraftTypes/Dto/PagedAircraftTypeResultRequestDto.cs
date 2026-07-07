using System;
using Abp.Application.Services.Dto;

namespace Selime.AircraftTypes.Dto
{
    public class PagedAircraftTypeResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string TypeCode { get; set; }
        public string Manufacturer { get; set; }
        public int? SeatCapacity { get; set; }
    }
}
