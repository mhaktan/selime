using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace Selime.Stations.Dto
{
    [AutoMapTo(typeof(Entities.Station))]
    public class CreateStationDto
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

    }
}