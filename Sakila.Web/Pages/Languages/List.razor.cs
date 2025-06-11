using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Languages;

public partial class List
{
    private IApiResponse<LanguageGetAllResponse>? _getAllResponse;
    private bool _isDeleteDialogOpen;
    private bool _isCreateDialogOpen;
    private bool _isUpdateDialogOpen;
    private LanguageGetByIdResponse _selectedLanguage = new();
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await RefreshLanguages();
    }

    private void ShowCreateDialog()
    {
        _isCreateDialogOpen = true;
    }

    private void ShowUpdateDialog(LanguageGetByIdResponse language)
    {
        _selectedLanguage = language;
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
        await RefreshLanguages();
        CloseCreateDialog();
    }

    private async Task OnUpdateSuccess()
    {
        await RefreshLanguages();
        CloseUpdateDialog();
    }

    private async Task RefreshLanguages()
    {
        await LanguageService.GetAllAsync(response => Task.FromResult(_getAllResponse = response));
    }

    private void ShowDeleteDialog(LanguageGetByIdResponse language)
    {
        _selectedLanguage = language;
        _isDeleteDialogOpen = true;
    }

    private void CloseDeleteDialog()
    {
        _isDeleteDialogOpen = false;
    }

    private async Task OnDeleteSuccess()
    {
        await RefreshLanguages();
        CloseDeleteDialog();
    }
}