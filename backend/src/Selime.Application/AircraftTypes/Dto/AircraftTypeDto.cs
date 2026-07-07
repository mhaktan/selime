using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Selime.AircraftTypes.Dto
{
    [AutoMapFrom(typeof(Entities.AircraftType))]
    public class AircraftTypeDto : EntityDto<long>
    {
        public string TypeCode { get; set; }

        public string Manufacturer { get; set; }

        public int? SeatCapacity { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}