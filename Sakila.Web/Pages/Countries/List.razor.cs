using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;
using Sakila.Web.Pages.Countries.Components;
using Sakila.Web.Extensions;

namespace Sakila.Web.Pages.Countries;

partial class List
{
    private ISakilaApiResponse<CountryGetAllResponse>? _getAllResponse;

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

    private Task ShowCreateDialog()
    {
        return DialogService.ShowDialogAsync<CountryCreateDialog>(
            "Add Country",
            onSuccess: RefreshCountries);
    }

    private Task ShowUpdateDialog(CountryGetByIdResponse country)
    {
        var parameters = new DialogParameters { [nameof(CountryUpdateDialog.Country)] = country };
        return DialogService.ShowDialogAsync<CountryUpdateDialog>(
            "Edit Country",
            parameters,
            RefreshCountries);
    }

    private Task ShowDeleteDialog(CountryGetByIdResponse country)
    {
        var parameters = new DialogParameters { [nameof(ConfirmDelete.Country)] = country };
        return DialogService.ShowDialogAsync<ConfirmDelete>(
            "Delete Country",
            parameters,
            RefreshCountries);
    }
}