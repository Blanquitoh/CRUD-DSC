using AutoMapper;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Domain.Models;

namespace Sakila.Application.Countries.Queries.Mapping;

public class GetByIdProfile : Profile
{
    public GetByIdProfile()
    {
        CreateMap<Country, CountryGetByIdResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CountryId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Country1))
            .ReverseMap();
    }
}