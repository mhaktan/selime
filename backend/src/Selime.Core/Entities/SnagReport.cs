using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Selime.Entities
{
    // State Machine: status — Open → InProgress → PendingCRS → Closed
    // Initial: Open | Transitions: Open→InProgress[Assign], InProgress→PendingCRS[Approve], InProgress→InProgress[Revise], PendingCRS→Closed[Approve], PendingCRS→InProgress[Revise]
    [Table("SnagReports")]
    public class SnagReport : FullAuditedEntity<long>
    {
        [Required]
        [MaxLength(50)]
        public string ReportNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public Severity Severity { get; set; }

        [Required]
        [MaxLength(200)]
        public string ReportedBy { get; set; }

        public DateTime DetectionDate { get; set; }

        [MaxLength(2000)]
        public string ActionTaken { get; set; }

        [MaxLength(100)]
        public string CrsNumber { get; set; }

        [MaxLength(1000)]
        public string RevisionNote { get; set; }

        public Status Status { get; set; }

        public long? LineMechanicId { get; set; }

        public long? CertifyingStaffId { get; set; }

        public long AircraftId { get; set; }

        [ForeignKey(nameof(AircraftId))]
        public virtual Aircraft Aircraft { get; set; }

        public long AtaChapterId { get; set; }

        [ForeignKey(nameof(AtaChapterId))]
        public virtual AtaChapter AtaChapter { get; set; }

        public long StationId { get; set; }

        [ForeignKey(nameof(StationId))]
        public virtual Station Station { get; set; }

        public virtual ICollection<SnagReportPart> SnagReportParts { get; set; }

    }
}