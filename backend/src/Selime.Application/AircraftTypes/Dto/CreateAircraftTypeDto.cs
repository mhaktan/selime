using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace Selime.AircraftTypes.Dto
{
    [AutoMapTo(typeof(Entities.AircraftType))]
    public class CreateAircraftTypeDto
    {
        [Required]
        [MaxLength(20)]
        public string TypeCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Manufacturer { get; set; }

        public int? SeatCapacity { get; set; }

    }
}