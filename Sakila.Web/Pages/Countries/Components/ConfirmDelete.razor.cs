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

    private async Task ConfirmAsync()
    {
        var success = false;
        await CountryService.DeleteAsync(Country.Id,
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
