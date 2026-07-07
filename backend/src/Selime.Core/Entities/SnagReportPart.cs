using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Selime.Entities
{
    [Table("SnagReportParts")]
    public class SnagReportPart : FullAuditedEntity<long>
    {
        [MaxLength(100)]
        public string SerialNumber { get; set; }

        public int QuantityUsed { get; set; }

        public long SnagReportId { get; set; }

        [ForeignKey(nameof(SnagReportId))]
        public virtual SnagReport SnagReport { get; set; }

        public long PartCatalogId { get; set; }

        [ForeignKey(nameof(PartCatalogId))]
        public virtual PartCatalog PartCatalog { get; set; }

    }
}