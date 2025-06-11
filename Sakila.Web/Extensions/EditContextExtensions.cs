using Microsoft.AspNetCore.Components.Forms;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Extensions;

public static class EditContextExtensions
{
    public static bool ApplyErrors(this EditContext editContext, ValidationMessageStore messageStore, IApiResponse<object>? response)
    {
        if (response == null)
            return true;

        messageStore.Clear();

        if (response.IsSuccess)
            return true;

        foreach (var (field, messages) in response.Errors)
        {
            messageStore.Add(editContext.Field(field), messages);
        }

        editContext.NotifyValidationStateChanged();
        return false;
    }
}

