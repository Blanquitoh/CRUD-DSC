using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Countries.Components;

public partial class ConfirmDelete
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public CountryGetByIdResponse Country { get; set; } = new();
    [Inject] public ICountryService CountryService { get; set; } = null!;

    public IApiResponse<object>? ApiResponse { get; set; }

    private async Task ConfirmAsync()
    {
        await CountryService.DeleteAsync(Country.Id,
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