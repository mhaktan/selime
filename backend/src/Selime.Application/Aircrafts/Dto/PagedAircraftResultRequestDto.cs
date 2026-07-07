using System;
using Abp.Application.Services.Dto;

namespace Selime.Aircrafts.Dto
{
    public class PagedAircraftResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? AircraftTypeId { get; set; }
        public long? StationId { get; set; }
        public string Registration { get; set; }
        public int? ManufacturingYear { get; set; }
        public int? Status { get; set; }
    }
}
