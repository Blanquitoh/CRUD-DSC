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

    public IApiResponse<object>? ApiResponse { get; set; }

    private async Task ConfirmAsync()
    {
        await LanguageService.DeleteAsync(Language.Id,
            response =>
            {
                ApiResponse = response;
                return Task.CompletedTask;
            });
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void Cancel()
    {
        MudDialog.Cancel();
    }
}