using AutoMapper;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;

namespace Sakila.Application.Languages.Commands.Mapping;

public class CreateProfile : Profile
{
    public CreateProfile()
    {
        CreateMap<LanguageCreateRequest, Language>();
    }
}