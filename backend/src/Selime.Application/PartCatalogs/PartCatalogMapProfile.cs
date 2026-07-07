using AutoMapper;
using Selime.Entities;
using Selime.PartCatalogs.Dto;

namespace Selime.PartCatalogs
{
    public class PartCatalogMapProfile : Profile
    {
        public PartCatalogMapProfile()
        {
            CreateMap<PartCatalog, PartCatalogDto>();
            CreateMap<CreatePartCatalogDto, PartCatalog>();
            CreateMap<PartCatalogDto, PartCatalog>();
        }
    }
}
