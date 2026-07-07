using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Selime.PartCatalogs.Dto
{
    [AutoMapFrom(typeof(Entities.PartCatalog))]
    public class PartCatalogDto : EntityDto<long>
    {
        public string PartNumber { get; set; }

        public string Description { get; set; }

        public string UnitOfMeasure { get; set; }

        public int StockQuantity { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}