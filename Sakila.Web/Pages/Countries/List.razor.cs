using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Countries;

public partial class List
{
    private IApiResponse<CountryGetAllResponse>? _getAllResponse;
    private bool _isDeleteDialogOpen;
    private bool _isDialogOpen;
    private IApiResponse<object>? _otherResponse;
    private CountryGetByIdResponse _selectedCountry = new();
    [Inject] public ICountryService CountryService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await RefreshCountries();
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

        _otherResponse = null;
        _isDialogOpen = true;
    }

    private void CloseDialog()
    {
        _isDialogOpen = false;
        _selectedCountry = new CountryGetByIdResponse();
        _otherResponse = null;
    }

    private async Task SubmitCountry()
    {
        _otherResponse = null;
        if (_selectedCountry.Id == 0)
        {
            var request = new CountryCreateRequest { Name = _selectedCountry.Name };
            _otherResponse = await CountryService.CreateAsync(request);
        }
        else
        {
            var request = new CountryUpdateRequest { Id = _selectedCountry.Id, Name = _selectedCountry.Name };
            _otherResponse = await CountryService.UpdateAsync(request);
        }

        if (_otherResponse.IsSuccess)
        {
            await RefreshCountries();
            CloseDialog();
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