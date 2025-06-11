using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Countries.Components;

public partial class CountryUpdateDialog
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public CountryGetByIdResponse Country { get; set; } = new();
    [Inject] public ICountryService CountryService { get; set; } = null!;

    private CountryUpdateRequest Model { get; set; } = new();

    protected override void OnParametersSet()
    {
        Model = new CountryUpdateRequest { Id = Country.Id, Name = Country.Name };
        CountryService.Initialize(Model);
    }

    private async Task SubmitAsync()
    {
        var success = false;
        await CountryService.UpdateAsync(Model,
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