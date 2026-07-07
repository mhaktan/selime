using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Selime.AtaChapters.Dto
{
    [AutoMapFrom(typeof(Entities.AtaChapter))]
    public class AtaChapterDto : EntityDto<long>
    {
        public string AtaNumber { get; set; }

        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

    }
}