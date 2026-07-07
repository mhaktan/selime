using System;
using Abp.Application.Services.Dto;

namespace Selime.SnagReportParts.Dto
{
    public class PagedSnagReportPartResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? SnagReportId { get; set; }
        public long? PartCatalogId { get; set; }
        public string SerialNumber { get; set; }
        public int? QuantityUsed { get; set; }
    }
}
