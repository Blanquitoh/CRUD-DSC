using AutoMapper;
using FluentValidation;
using Sakila.Application.Common.Handlers;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Application.Common.Interfaces;

namespace Sakila.Application.Languages.Commands.Handlers;

public class CreateHandler(
    ISakilaContext dbContext,
    IMapper mapper,
    IValidator<LanguageCreateRequest> validator)
    : CreateHandlerBase<LanguageCreateRequest, Language, int>(dbContext, mapper, validator)
{
    protected override int GetResponse(Language entity)
    {
        return entity.LanguageId;
    }
}