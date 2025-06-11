using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;
using Sakila.Web.Pages.Countries.Components;

namespace Sakila.Web.Pages.Countries
{
    partial class List
    {
        private IApiResponse<CountryGetAllResponse>? _getAllResponse;

        [Inject] public ICountryService CountryService { get; set; } = null!;
        [Inject] public IDialogService DialogService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await RefreshCountries();
        }

        private async Task RefreshCountries()
        {
            await CountryService.GetAllAsync(r => Task.FromResult(_getAllResponse = r));
        }

        private async Task ShowCreateDialog()
        {
            var dialog = await DialogService.ShowAsync<CountryCreateDialog>("Add Country");
            var result = (await dialog.Result)!;
            if (!result.Canceled)
            {
                await RefreshCountries();
            }
        }

        private async Task ShowUpdateDialog(CountryGetByIdResponse country)
        {
            var parameters = new DialogParameters { [nameof(CountryUpdateDialog.Country)] = country };
            var dialog = await DialogService.ShowAsync<CountryUpdateDialog>("Edit Country", parameters);
            var result = (await dialog.Result)!;
            if (!result.Canceled)
            {
                await RefreshCountries();
            }
        }

        private async Task ShowDeleteDialog(CountryGetByIdResponse country)
        {
            var parameters = new DialogParameters { [nameof(ConfirmDelete.Country)] = country };
            var dialog = await DialogService.ShowAsync<ConfirmDelete>("Delete Country", parameters);
            var result = (await dialog.Result)!;
            if (!result.Canceled)
            {
                await RefreshCountries();
            }
        }
    }
}
