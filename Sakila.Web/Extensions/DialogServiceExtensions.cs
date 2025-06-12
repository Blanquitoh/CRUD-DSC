using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Sakila.Web.Extensions;

public static class DialogServiceExtensions
{
    public static async Task<IDialogReference> ShowDialogAsync<TDialog>(
        this IDialogService dialogService,
        string title,
        DialogParameters? parameters = null,
        Func<Task>? onSuccess = null,
        Func<Task>? onCancel = null)
        where TDialog : IComponent
    {
        var dialog = await dialogService.ShowAsync<TDialog>(title, parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            if (onSuccess != null)
                await onSuccess();
        }
        else if (onCancel != null)
        {
            await onCancel();
        }

        return dialog;
    }
}
