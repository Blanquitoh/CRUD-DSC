using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Languages.Components;

public partial class ConfirmDelete
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public LanguageGetByIdResponse Language { get; set; } = new();
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    protected override void OnInitialized()
    {
        LanguageService.Initialize(Language);
    }

    private async Task ConfirmAsync()
    {
        var success = false;
        await LanguageService.DeleteAsync(Language.Id,
            _ =>
            {
                success = true;
                return Task.CompletedTask;
            });

        if (success)
            MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }
}