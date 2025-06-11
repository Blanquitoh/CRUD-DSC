using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Countries.Commands;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Countries.Components;

public partial class CountryCreateDialog
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;
    [Inject] public ICountryService CountryService { get; set; } = null!;

    public IApiResponse<object>? ApiResponse { get; set; }
    private CountryCreateRequest Country { get; } = new();

    protected override void OnInitialized()
    {
        CountryService.Initialize(Country);
    }

    private async Task SubmitAsync()
    {
        await CountryService.CreateAsync(Country,
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