using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Countries.Commands;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Countries.Components;

public partial class CountryCreateDialog
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
    [Inject] public ICountryService CountryService { get; set; } = null!;

    private CountryCreateRequest Country { get; } = new();

    protected override void OnInitialized()
    {
        CountryService.Initialize(Country);
    }

    private async Task SubmitAsync()
    {
        var success = false;
        await CountryService.CreateAsync(Country,
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