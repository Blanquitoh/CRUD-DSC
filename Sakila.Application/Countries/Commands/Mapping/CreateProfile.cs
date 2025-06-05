using AutoMapper;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;

namespace Sakila.Application.Countries.Commands.Mapping;

public class CreateProfile : Profile
{
    public CreateProfile()
    {
        CreateMap<CountryCreateRequest, Country>()
            .ForMember(dest => dest.Country1, opt => opt.MapFrom(src => src.Name));
    }
}
