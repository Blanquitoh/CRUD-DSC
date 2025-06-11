using Microsoft.AspNetCore.Components.Forms;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Extensions;

public static class EditContextExtensions
{
    public static void ApplyErrors(this EditContext editContext, ValidationMessageStore messageStore,
        ISakilaApiResponse<object>? response)
    {
        if (response == null) return;

        messageStore.Clear();

        if (response.IsSuccess) return;


        foreach (var (field, messages) in response.Errors)
            messageStore.Add(editContext.Field(field), messages);

        foreach (var message in response.GeneralErrors)
            messageStore.Add(editContext.Field(string.Empty), new[] { message });

        editContext.NotifyValidationStateChanged();
    }

    public static void ApplyErrors<TGetById>(this EditContext editContext, ValidationMessageStore messageStore,
        ISakilaApiResponse<TGetById> response)
    {
        messageStore.Clear();

        if (response.IsSuccess) return;

        foreach (var (field, messages) in response.Errors)
            messageStore.Add(editContext.Field(field), messages);

        foreach (var message in response.GeneralErrors)
            messageStore.Add(editContext.Field(string.Empty), new[] { message });

        editContext.NotifyValidationStateChanged();
    }
}