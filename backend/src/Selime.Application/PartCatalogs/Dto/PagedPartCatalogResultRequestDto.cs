using System;
using Abp.Application.Services.Dto;

namespace Selime.PartCatalogs.Dto
{
    public class PagedPartCatalogResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasure { get; set; }
        public int? StockQuantity { get; set; }
    }
}
