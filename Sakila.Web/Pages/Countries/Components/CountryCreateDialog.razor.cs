using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Countries.Commands;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Countries.Components;

public partial class CountryCreateDialog
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSuccess { get; set; }
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
            async response =>
            {
                ApiResponse = response;
                await OnSuccess.InvokeAsync();
            });
    }
}