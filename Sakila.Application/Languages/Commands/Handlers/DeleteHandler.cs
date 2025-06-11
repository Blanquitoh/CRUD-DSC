using Sakila.Application.Common.Handlers;
using Sakila.Application.Common.Validation;
using Sakila.Application.Languages.Commands.Validators.Data;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class DeleteHandler(
    SakilaContext dbContext,
    IValidatorWithData<LanguageDeleteRequest, LanguageDeleteValidatorData> validator)
    : DeleteHandlerBase<LanguageDeleteRequest, Language, LanguageDeleteValidatorData>(dbContext, validator)
{
    protected override Language GetData(LanguageDeleteValidatorData data)
    {
        return data.Language;
    }
}