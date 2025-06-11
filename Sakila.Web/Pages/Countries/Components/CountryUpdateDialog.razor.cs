using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Countries.Commands;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Countries.Components;

public partial class CountryUpdateDialog
{
    [Parameter] public bool Visible { get; set; }
    [Parameter] public CountryGetByIdResponse Country { get; set; } = new();
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSuccess { get; set; }
    [Inject] public ICountryService CountryService { get; set; } = null!;

    public IApiResponse<object>? ApiResponse { get; set; }
    private CountryUpdateRequest Model { get; set; } = new();

    protected override void OnParametersSet()
    {
        Model = new CountryUpdateRequest { Id = Country.Id, Name = Country.Name };
    }

    private async Task SubmitAsync()
    {
        ApiResponse = await CountryService.UpdateAsync(Model);
        if (ApiResponse.IsSuccess)
        {
            await OnSuccess.InvokeAsync();
        }
    }
}

