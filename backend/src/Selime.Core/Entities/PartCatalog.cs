using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Selime.Entities
{
    [Table("PartCatalogs")]
    public class PartCatalog : FullAuditedEntity<long>
    {
        [Required]
        [MaxLength(100)]
        public string PartNumber { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string UnitOfMeasure { get; set; }

        public int StockQuantity { get; set; }

        public virtual ICollection<SnagReportPart> SnagReportParts { get; set; }

    }
}