using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Selime.Entities
{
    [Table("Stations")]
    public class Station : FullAuditedEntity<long>
    {
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        public virtual ICollection<Aircraft> Aircrafts { get; set; }

        public virtual ICollection<SnagReport> SnagReports { get; set; }

    }
}