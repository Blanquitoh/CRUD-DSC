using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Languages.Commands;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Languages.Components;

public partial class LanguageCreateDialog
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    private LanguageCreateRequest Language { get; } = new();

    protected override void OnInitialized()
    {
        LanguageService.Initialize(Language);
    }

    private async Task SubmitAsync()
    {
        await LanguageService.CreateAsync(Language,
            _ =>
            {
                MudDialog.Close(DialogResult.Ok(true));
                return Task.CompletedTask;
            }
        );
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }
}