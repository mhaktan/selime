using System;
using Abp.Application.Services.Dto;

namespace Selime.SnagReports.Dto
{
    public class PagedSnagReportResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? AircraftId { get; set; }
        public long? AtaChapterId { get; set; }
        public long? StationId { get; set; }
        public string ReportNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Severity { get; set; }
        public string ReportedBy { get; set; }
        public DateTime? DetectionDate { get; set; }
        public string ActionTaken { get; set; }
        public string CrsNumber { get; set; }
        public string RevisionNote { get; set; }
        public int? Status { get; set; }
        public long? LineMechanicId { get; set; }
        public long? CertifyingStaffId { get; set; }
    }
}
