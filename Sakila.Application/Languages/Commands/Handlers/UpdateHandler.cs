using AutoMapper;
using MediatR;
using Sakila.Application.Common.Handlers;
using Sakila.Application.Common.Validation;
using Sakila.Application.Languages.Commands.Validators.Data;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class UpdateHandler(
    SakilaContext dbContext,
    IMapper mapper,
    IValidatorWithData<LanguageUpdateRequest, LanguageUpdateValidatorData> validator)
    : UpdateHandlerBase<LanguageUpdateRequest, Language, LanguageUpdateValidatorData>(dbContext, mapper, validator)
{
    protected override Language GetData(LanguageUpdateValidatorData data) => data.Language;
}