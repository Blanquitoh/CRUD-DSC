using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Common;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Contracts.Services;
using Sakila.Web.Common;

namespace Sakila.Web.Pages.Countries;

public partial class List
{
    private bool _isDeleteDialogOpen;
    private bool _isDialogOpen;
    private IApiResponse<CountryGetAllResponse> _apiResponse;
    private CountryGetByIdResponse _selectedCountry = new();
    [Inject] public ICountryService CountryService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        _apiResponse = await CountryService.GetAllAsync();
    }

    private void ShowDialog(CountryGetByIdResponse? country = null)
    {
        if (country is null)
            _selectedCountry = new CountryGetByIdResponse();
        else
            _selectedCountry = new CountryGetByIdResponse
            {
                Id = country.Id,
                Name = country.Name
            };

        _isDialogOpen = true;
    }

    private void CloseDialog()
    {
        _isDialogOpen = false;
        _selectedCountry = new CountryGetByIdResponse();
    }

    private async Task SubmitCountry()
    {
        IApiResponse<object> apiResponse;
        if (_selectedCountry.Id == 0)
        {
            var request = new CountryCreateRequest { Name = _selectedCountry.Name };
            apiResponse = await CountryService.CreateAsync(request);
        }
        else
        {
            var request = new CountryUpdateRequest { Id = _selectedCountry.Id, Name = _selectedCountry.Name };
            apiResponse = await CountryService.UpdateAsync(request);
        }

        if (apiResponse.IsSuccess)
        {
            await RefreshCountries();
            CloseDialog();
        }
    }

    private async Task RefreshCountries()
    {
        _apiResponse = await CountryService.GetAllAsync();
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

    private async Task ConfirmDelete()
    {
        var apiResponse = await CountryService.DeleteAsync(_selectedCountry.Id);
        if (apiResponse.IsSuccess)
        {
            await RefreshCountries();
            CloseDeleteDialog();
        }
    }
}