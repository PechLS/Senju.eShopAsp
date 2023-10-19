using AutoMapper;
using eShopAsp.Core.Entities;
using eShopAsp.Core.Entities.CatalogItemAggregate;
using eShopAsp.UseCases.CatalogBrands;
using eShopAsp.UseCases.CatalogItems;
using eShopAsp.UseCases.CatalogTypes;

namespace eShopAsp.PublicApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogItem, CatalogItemDTO>();
        CreateMap<CatalogType, CatalogTypeDTO>()
            .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Type));
        CreateMap<CatalogBrand, CatalogBrandDTO>()
            .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Brand));
    }
}