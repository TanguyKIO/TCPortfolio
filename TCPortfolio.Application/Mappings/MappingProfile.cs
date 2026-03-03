using AutoMapper;
using TCPortfolio.Domain.Entities; 
using TCPortfolio.Application.DTOs; 

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Photo, PhotoDto>();
    }
}