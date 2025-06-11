using AutoMapper;
using FluentValidation;
using MediatR;
using Sakila.Application.Common.Handlers;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class CreateHandler(
    SakilaContext dbContext,
    IMapper mapper,
    IValidator<LanguageCreateRequest> validator)
    : CreateHandlerBase<LanguageCreateRequest, Language, int>(dbContext, mapper, validator)
{
    protected override int GetResponse(Language entity) => entity.LanguageId;
}