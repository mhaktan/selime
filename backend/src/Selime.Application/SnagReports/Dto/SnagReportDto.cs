using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Selime.SnagReports.Dto
{
    [AutoMapFrom(typeof(Entities.SnagReport))]
    public class SnagReportDto : EntityDto<long>
    {
        public string ReportNumber { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Severity { get; set; }

        public string ReportedBy { get; set; }

        public DateTime DetectionDate { get; set; }

        public string ActionTaken { get; set; }

        public string CrsNumber { get; set; }

        public string RevisionNote { get; set; }

        public int Status { get; set; }

        public long? LineMechanicId { get; set; }

        public long? CertifyingStaffId { get; set; }

        /// <summary>
        /// String form of the status — used by flow conditions (triggerData.statusName equals "PendingX").
        /// </summary>
        public string StatusName { get; set; }

        public long AircraftId { get; set; }

        public long AtaChapterId { get; set; }

        public long StationId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}