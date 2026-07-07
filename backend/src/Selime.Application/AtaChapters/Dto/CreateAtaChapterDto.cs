using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace Selime.AtaChapters.Dto
{
    [AutoMapTo(typeof(Entities.AtaChapter))]
    public class CreateAtaChapterDto
    {
        [Required]
        [MaxLength(10)]
        public string AtaNumber { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

    }
}