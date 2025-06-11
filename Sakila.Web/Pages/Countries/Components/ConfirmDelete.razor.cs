using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Countries.Components;

public partial class ConfirmDelete
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public CountryGetByIdResponse Country { get; set; } = new();
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSuccess { get; set; }
    [Inject] public ICountryService CountryService { get; set; } = null!;

    public IApiResponse<object>? ApiResponse { get; set; }

    private async Task ConfirmAsync()
    {
        ApiResponse = await CountryService.DeleteAsync(Country.Id);
        if (ApiResponse.IsSuccess)
        {
            await OnSuccess.InvokeAsync();
        }
    }
}