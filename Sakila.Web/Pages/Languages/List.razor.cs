using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;
using Fluxor;
using Sakila.Web.Store.Languages;

namespace Sakila.Web.Pages.Languages;

public partial class List
{
    private IApiResponse<LanguageGetAllResponse>? _getAllResponse;
    [Inject] public IState<LanguageState> LanguageState { get; set; } = null!;
    [Inject] public IDispatcher Dispatcher { get; set; } = null!;
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await RefreshLanguages();
    }

    private void ShowCreateDialog()
    {
        Dispatcher.Dispatch(new ShowCreateDialogAction());
    }

    private void ShowUpdateDialog(LanguageGetByIdResponse language)
    {
        Dispatcher.Dispatch(new ShowUpdateDialogAction(language));
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
        await RefreshLanguages();
        Dispatcher.Dispatch(new CloseCreateDialogAction());
    }

    private async Task OnUpdateSuccess()
    {
        await RefreshLanguages();
        Dispatcher.Dispatch(new CloseUpdateDialogAction());
    }

    private async Task RefreshLanguages()
    {
        await LanguageService.GetAllAsync(response => Task.FromResult(_getAllResponse = response));
    }

    private void ShowDeleteDialog(LanguageGetByIdResponse language)
    {
        Dispatcher.Dispatch(new ShowDeleteDialogAction(language));
    }

    private void CloseDeleteDialog()
    {
        Dispatcher.Dispatch(new CloseDeleteDialogAction());
    }

    private async Task OnDeleteSuccess()
    {
        await RefreshLanguages();
        Dispatcher.Dispatch(new CloseDeleteDialogAction());
    }
}