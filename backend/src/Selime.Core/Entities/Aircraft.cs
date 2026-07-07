using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Selime.Entities
{
    [Table("Aircrafts")]
    public class Aircraft : FullAuditedEntity<long>
    {
        [Required]
        [MaxLength(20)]
        public string Registration { get; set; }

        public int ManufacturingYear { get; set; }

        public Status Status { get; set; }

        public long AircraftTypeId { get; set; }

        [ForeignKey(nameof(AircraftTypeId))]
        public virtual AircraftType AircraftType { get; set; }

        public long StationId { get; set; }

        [ForeignKey(nameof(StationId))]
        public virtual Station Station { get; set; }

        public virtual ICollection<SnagReport> SnagReports { get; set; }

    }
}