using AutoMapper;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;

namespace Sakila.Application.Languages.Commands.Mapping
{
    public class UpdateProfile : Profile
    {
        public UpdateProfile()
        {
            CreateMap<LanguageUpdateRequest, Language>().ForMember(
                dest => dest.LanguageId, opt => opt.MapFrom(src => src.Id)
            );
        }
    }
}