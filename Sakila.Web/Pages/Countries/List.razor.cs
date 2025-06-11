using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Countries;

public partial class List
{
    private IApiResponse<CountryGetAllResponse>? _getAllResponse;
    private bool _isDeleteDialogOpen;
    private bool _isCreateDialogOpen;
    private bool _isUpdateDialogOpen;
    private IApiResponse<object>? _otherResponse;
    private CountryGetByIdResponse _selectedCountry = new();
    private CountryCreateRequest _createCountry = new();
    private CountryUpdateRequest _updateCountry = new();
    [Inject] public ICountryService CountryService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await RefreshCountries();
    }

    private void ShowCreateDialog()
    {
        _createCountry = new CountryCreateRequest();
        _otherResponse = null;
        _isCreateDialogOpen = true;
    }

    private void ShowUpdateDialog(CountryGetByIdResponse country)
    {
        _updateCountry = new CountryUpdateRequest
        {
            Id = country.Id,
            Name = country.Name
        };
        _otherResponse = null;
        _isUpdateDialogOpen = true;
    }

    private void CloseCreateDialog()
    {
        _isCreateDialogOpen = false;
        _createCountry = new CountryCreateRequest();
        _otherResponse = null;
    }

    private void CloseUpdateDialog()
    {
        _isUpdateDialogOpen = false;
        _updateCountry = new CountryUpdateRequest();
        _otherResponse = null;
    }

    private async Task SubmitCreateRequest(CountryCreateRequest request)
    {
        _otherResponse = await CountryService.CreateAsync(request);
        if (_otherResponse.IsSuccess)
        {
            await RefreshCountries();
            CloseCreateDialog();
        }
    }

    private async Task SubmitUpdateRequest(CountryUpdateRequest request)
    {
        _otherResponse = await CountryService.UpdateAsync(request);
        if (_otherResponse.IsSuccess)
        {
            await RefreshCountries();
            CloseUpdateDialog();
        }
    }

    private async Task RefreshCountries()
    {
        _getAllResponse = await CountryService.GetAllAsync();
    }

    private void ShowDeleteDialog(CountryGetByIdResponse country)
    {
        _selectedCountry = country;
        _isDeleteDialogOpen = true;
        _otherResponse = null;
    }

    private void CloseDeleteDialog()
    {
        _isDeleteDialogOpen = false;
        _otherResponse = null;
    }

    private async Task ConfirmDelete()
    {
        _otherResponse = await CountryService.DeleteAsync(_selectedCountry.Id);
        if (_otherResponse.IsSuccess)
        {
            await RefreshCountries();
            CloseDeleteDialog();
        }
    }
}