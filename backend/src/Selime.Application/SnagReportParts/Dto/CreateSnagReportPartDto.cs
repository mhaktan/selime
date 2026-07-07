using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace Selime.SnagReportParts.Dto
{
    [AutoMapTo(typeof(Entities.SnagReportPart))]
    public class CreateSnagReportPartDto
    {
        [MaxLength(100)]
        public string SerialNumber { get; set; }

        public int QuantityUsed { get; set; }

        public long SnagReportId { get; set; }

        public long PartCatalogId { get; set; }

    }
}