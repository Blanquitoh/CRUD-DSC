using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Responses;
using Sakila.Contracts.Services;
using Sakila.Web.Common;

namespace Sakila.Web.Pages.Countries;

public partial class List
{
    private bool _isDeleteDialogOpen;
    private bool _isDialogOpen;
    private CountryGetAllResponse? _params;
    private CountryGetByIdResponse _selectedCountry = new();
    [Inject] public ICountryService CountryService { get; set; } = null!;
    private Dictionary<string, string[]> Errors { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        _params = await CountryService.GetAllAsync();
    }

    private void ShowDialog(CountryGetByIdResponse? country = null)
    {
        if (country is null)
        {
            _selectedCountry = new CountryGetByIdResponse();
        }
        else
        {
            _selectedCountry = new CountryGetByIdResponse
            {
                Id = country.Id,
                Name = country.Name
            };
        }

        Errors = new();
        _isDialogOpen = true;
    }

    private void CloseDialog()
    {
        _isDialogOpen = false;
        _selectedCountry = new CountryGetByIdResponse();
        Errors = new();
    }

    private async Task SubmitCountry()
    {
        try
        {
            if (_selectedCountry.Id == 0)
            {
                var request = new CountryCreateRequest { Name = _selectedCountry.Name };
                await CountryService.CreateAsync(request);
            }
            else
            {
                var request = new CountryUpdateRequest { Id = _selectedCountry.Id, Name = _selectedCountry.Name };
                await CountryService.UpdateAsync(request);
            }
            await RefreshCountries();
            CloseDialog();
        }
        catch (ApiValidationException exception)
        {
            Errors = exception.Errors;
        }
    }

    private async Task RefreshCountries()
    {
        _params = await CountryService.GetAllAsync();
    }

    private void ShowDeleteDialog(CountryGetByIdResponse country)
    {
        _selectedCountry = country;
        Errors = new();
        _isDeleteDialogOpen = true;
    }

    private void CloseDeleteDialog()
    {
        _isDeleteDialogOpen = false;
        Errors = new();
    }

    private async Task ConfirmDelete()
    {
        try
        {
            await CountryService.DeleteAsync(_selectedCountry.Id);
            await RefreshCountries();
            CloseDeleteDialog();
        }
        catch (ApiValidationException exception)
        {
            Errors = exception.Errors;
        }
    }
}
