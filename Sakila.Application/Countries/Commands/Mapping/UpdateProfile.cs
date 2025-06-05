using AutoMapper;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;

namespace Sakila.Application.Countries.Commands.Mapping;

public class UpdateProfile : Profile
{
    public UpdateProfile()
    {
        CreateMap<CountryUpdateRequest, Country>()
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Country1, opt => opt.MapFrom(src => src.Name));
    }
}