using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Pages.Languages;

public partial class List
{
    private IApiResponse<LanguageGetAllResponse>? _getAllResponse;
    private bool _isDeleteDialogOpen;
    private bool _isCreateDialogOpen;
    private bool _isUpdateDialogOpen;
    private IApiResponse<object>? _otherResponse;
    private LanguageGetByIdResponse _selectedLanguage = new();
    private LanguageCreateRequest _createLanguage = new();
    private LanguageUpdateRequest _updateLanguage = new();
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await RefreshLanguages();
    }

    private void ShowCreateDialog()
    {
        _createLanguage = new LanguageCreateRequest();
        _otherResponse = null;
        _isCreateDialogOpen = true;
    }

    private void ShowUpdateDialog(LanguageGetByIdResponse language)
    {
        _updateLanguage = new LanguageUpdateRequest
        {
            Id = language.Id,
            Name = language.Name
        };
        _otherResponse = null;
        _isUpdateDialogOpen = true;
    }

    private void CloseCreateDialog()
    {
        _isCreateDialogOpen = false;
        _createLanguage = new LanguageCreateRequest();
        _otherResponse = null;
    }

    private void CloseUpdateDialog()
    {
        _isUpdateDialogOpen = false;
        _updateLanguage = new LanguageUpdateRequest();
        _otherResponse = null;
    }

    private async Task SubmitCreateRequest(LanguageCreateRequest request)
    {
        _otherResponse = await LanguageService.CreateAsync(request);
        if (_otherResponse.IsSuccess)
        {
            await RefreshLanguages();
            CloseCreateDialog();
        }
    }

    private async Task SubmitUpdateRequest(LanguageUpdateRequest request)
    {
        _otherResponse = await LanguageService.UpdateAsync(request);
        if (_otherResponse.IsSuccess)
        {
            await RefreshLanguages();
            CloseUpdateDialog();
        }
    }

    private async Task RefreshLanguages()
    {
        _getAllResponse = await LanguageService.GetAllAsync();
    }

    private void ShowDeleteDialog(LanguageGetByIdResponse language)
    {
        _selectedLanguage = language;
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
        _otherResponse = await LanguageService.DeleteAsync(_selectedLanguage.Id);
        if (_otherResponse.IsSuccess)
        {
            await RefreshLanguages();
            CloseDeleteDialog();
        }
    }
}