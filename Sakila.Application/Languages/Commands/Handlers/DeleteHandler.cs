using Sakila.Application.Common.Handlers;
using Sakila.Application.Common.Validation;
using Sakila.Application.Languages.Commands.Validators.Data;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Application.Common.Interfaces;

namespace Sakila.Application.Languages.Commands.Handlers;

public class DeleteHandler(
    ISakilaContext dbContext,
    IValidatorWithData<LanguageDeleteRequest, LanguageDeleteValidatorData> validator)
    : DeleteHandlerBase<LanguageDeleteRequest, Language, LanguageDeleteValidatorData>(dbContext, validator)
{
    protected override Language GetData(LanguageDeleteValidatorData data)
    {
        return data.Language;
    }
}