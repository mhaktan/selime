using AutoMapper;
using Selime.Entities;
using Selime.AtaChapters.Dto;

namespace Selime.AtaChapters
{
    public class AtaChapterMapProfile : Profile
    {
        public AtaChapterMapProfile()
        {
            CreateMap<AtaChapter, AtaChapterDto>();
            CreateMap<CreateAtaChapterDto, AtaChapter>();
            CreateMap<AtaChapterDto, AtaChapter>();
        }
    }
}
