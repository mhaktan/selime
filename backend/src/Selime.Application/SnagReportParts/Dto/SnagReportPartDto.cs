using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Selime.SnagReportParts.Dto
{
    [AutoMapFrom(typeof(Entities.SnagReportPart))]
    public class SnagReportPartDto : EntityDto<long>
    {
        public string SerialNumber { get; set; }

        public int QuantityUsed { get; set; }

        public long SnagReportId { get; set; }

        public long PartCatalogId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}