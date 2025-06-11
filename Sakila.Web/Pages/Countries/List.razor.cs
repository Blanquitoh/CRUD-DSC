using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;
using Fluxor;
using Sakila.Web.Store.Countries;

namespace Sakila.Web.Pages.Countries;

public partial class List
{
    private IApiResponse<CountryGetAllResponse>? _getAllResponse;
    [Inject] public IState<CountryState> CountryState { get; set; } = null!;
    [Inject] public IDispatcher Dispatcher { get; set; } = null!;
    [Inject] public ICountryService CountryService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await RefreshCountries();
    }

    private void ShowCreateDialog()
    {
        Dispatcher.Dispatch(new ShowCreateDialogAction());
    }

    private void ShowUpdateDialog(CountryGetByIdResponse country)
    {
        Dispatcher.Dispatch(new ShowUpdateDialogAction(country));
    }

    private void CloseCreateDialog()
    {
        Dispatcher.Dispatch(new CloseCreateDialogAction());
    }

    private void CloseUpdateDialog()
    {
        Dispatcher.Dispatch(new CloseUpdateDialogAction());
    }

    private async Task OnCreateSuccess()
    {
        await RefreshCountries();
        Dispatcher.Dispatch(new CloseCreateDialogAction());
    }

    private async Task OnUpdateSuccess()
    {
        await RefreshCountries();
        Dispatcher.Dispatch(new CloseUpdateDialogAction());
    }

    private async Task RefreshCountries()
    {
        await CountryService.GetAllAsync(
            async response => _getAllResponse = response);
    }

    private void ShowDeleteDialog(CountryGetByIdResponse country)
    {
        Dispatcher.Dispatch(new ShowDeleteDialogAction(country));
    }

    private void CloseDeleteDialog()
    {
        Dispatcher.Dispatch(new CloseDeleteDialogAction());
    }

    private async Task OnDeleteSuccess()
    {
        await RefreshCountries();
        Dispatcher.Dispatch(new CloseDeleteDialogAction());
    }
}