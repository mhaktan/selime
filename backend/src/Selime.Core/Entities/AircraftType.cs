using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Selime.Entities
{
    [Table("AircraftTypes")]
    public class AircraftType : FullAuditedEntity<long>
    {
        [Required]
        [MaxLength(20)]
        public string TypeCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Manufacturer { get; set; }

        public int? SeatCapacity { get; set; }

        public virtual ICollection<Aircraft> Aircrafts { get; set; }

    }
}