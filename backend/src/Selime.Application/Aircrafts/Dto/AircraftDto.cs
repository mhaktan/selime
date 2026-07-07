using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Selime.Aircrafts.Dto
{
    [AutoMapFrom(typeof(Entities.Aircraft))]
    public class AircraftDto : EntityDto<long>
    {
        public string Registration { get; set; }

        public int ManufacturingYear { get; set; }

        public int Status { get; set; }

        public long AircraftTypeId { get; set; }

        public long StationId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}