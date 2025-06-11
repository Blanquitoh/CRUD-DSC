using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Languages.Components;

public partial class LanguageUpdateDialog
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public LanguageGetByIdResponse Language { get; set; } = new();
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    private LanguageUpdateRequest Model { get; set; } = new();

    protected override void OnParametersSet()
    {
        Model = new LanguageUpdateRequest { Id = Language.Id, Name = Language.Name };
        LanguageService.Initialize(Model);
    }

    private async Task SubmitAsync()
    {
        var success = false;
        await LanguageService.UpdateAsync(Model,
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