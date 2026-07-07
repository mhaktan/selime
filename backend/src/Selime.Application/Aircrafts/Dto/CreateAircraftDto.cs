using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace Selime.Aircrafts.Dto
{
    [AutoMapTo(typeof(Entities.Aircraft))]
    public class CreateAircraftDto
    {
        [Required]
        [MaxLength(20)]
        public string Registration { get; set; }

        public int ManufacturingYear { get; set; }

        public int Status { get; set; }

        public long AircraftTypeId { get; set; }

        public long StationId { get; set; }

    }
}