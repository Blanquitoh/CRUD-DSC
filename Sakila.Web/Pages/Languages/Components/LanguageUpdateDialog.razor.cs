using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Languages.Components;

public partial class LanguageUpdateDialog
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
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
        await LanguageService.UpdateAsync(Model,
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