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
    private CountryCreateRequest Country { get; set; } = new();

    private async Task SubmitAsync()
    {
        ApiResponse = await CountryService.CreateAsync(Country);
        if (ApiResponse.IsSuccess)
        {
            await OnSuccess.InvokeAsync();
        }
    }
}

