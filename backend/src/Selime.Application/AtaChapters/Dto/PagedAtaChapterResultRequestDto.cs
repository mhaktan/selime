using System;
using Abp.Application.Services.Dto;

namespace Selime.AtaChapters.Dto
{
    public class PagedAtaChapterResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public string AtaNumber { get; set; }
        public string Name { get; set; }
    }
}
