using AutoMapper;
using Sakila.Contracts.Languages.Responses;
using Sakila.Domain.Models;

namespace Sakila.Application.Languages.Queries.Mapping;

public class GetByIdProfile : Profile
{
    public GetByIdProfile()
    {
        CreateMap<Language, LanguageGetByIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.LanguageId)).ReverseMap();
    }
}