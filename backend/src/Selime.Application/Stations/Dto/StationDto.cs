using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Selime.Stations.Dto
{
    [AutoMapFrom(typeof(Entities.Station))]
    public class StationDto : EntityDto<long>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}