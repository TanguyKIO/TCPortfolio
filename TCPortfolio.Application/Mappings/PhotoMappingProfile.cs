using AutoMapper;
using AutoMapper.EquivalencyExpression;
using TCPortfolio.Application.DTOs;
using TCPortfolio.Domain.Entities; 

public class PhotoMappingProfile : Profile
{
    public PhotoMappingProfile()
    {
        CreateMap<PhotoTranslationDto, PhotoTranslation>()
            // If same lang, same entity, otherwise create new
            .EqualityComparison((dto, entity) => dto.Lang == entity.Lang);

        CreateMap<CreateUpdatePhotoDto, Photo>()
            // UseDestinationValue to keep existing translations and update them, instead of replacing the whole collection
            .ForMember(dest => dest.Translations, opt => opt.UseDestinationValue())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
            
        CreateMap<CreateUpdatePhotoDto, Photo>();
    }
}