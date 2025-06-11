using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Countries;

public partial class List
{
    private IApiResponse<CountryGetAllResponse>? _getAllResponse;
    private bool _isDeleteDialogOpen;
    private bool _isCreateDialogOpen;
    private bool _isUpdateDialogOpen;
    private CountryGetByIdResponse _selectedCountry = new();
    [Inject] public ICountryService CountryService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await RefreshCountries();
    }

    private void ShowCreateDialog()
    {
        _isCreateDialogOpen = true;
    }

    private void ShowUpdateDialog(CountryGetByIdResponse country)
    {
        _selectedCountry = country;
        _isUpdateDialogOpen = true;
    }

    private void CloseCreateDialog()
    {
        _isCreateDialogOpen = false;
    }

    private void CloseUpdateDialog()
    {
        _isUpdateDialogOpen = false;
    }

    private async Task OnCreateSuccess()
    {
        await RefreshCountries();
        CloseCreateDialog();
    }

    private async Task OnUpdateSuccess()
    {
        await RefreshCountries();
        CloseUpdateDialog();
    }

    private async Task RefreshCountries()
    {
        await CountryService.GetAllAsync(response => Task.FromResult(_getAllResponse = response));
    }

    private void ShowDeleteDialog(CountryGetByIdResponse country)
    {
        _selectedCountry = country;
        _isDeleteDialogOpen = true;
    }

    private void CloseDeleteDialog()
    {
        _isDeleteDialogOpen = false;
    }

    private async Task OnDeleteSuccess()
    {
        await RefreshCountries();
        CloseDeleteDialog();
    }
}