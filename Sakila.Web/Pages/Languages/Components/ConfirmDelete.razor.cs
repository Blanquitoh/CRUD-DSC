using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Languages.Components;

public partial class ConfirmDelete
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    [Parameter] public LanguageGetByIdResponse Language { get; set; } = new();
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    private async Task ConfirmAsync()
    {
        await LanguageService.DeleteAsync(Language.Id,
            _ =>
            {
                MudDialog.Close(DialogResult.Ok(true));
                return Task.CompletedTask;
            });
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }
}