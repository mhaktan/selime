using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Selime.Entities
{
    [Table("AtaChapters")]
    public class AtaChapter : FullAuditedEntity<long>
    {
        [Required]
        [MaxLength(10)]
        public string AtaNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual ICollection<SnagReport> SnagReports { get; set; }

    }
}