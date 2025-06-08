using Microsoft.AspNetCore.Components;
using Sakila.Contracts.Common;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Contracts.Services;

namespace Sakila.Web.Pages.Languages;

public partial class List
{
    private IApiResponse<LanguageGetAllResponse>? _getAllResponse;
    private bool _isDeleteDialogOpen;
    private bool _isDialogOpen;
    private IApiResponse<object>? _otherResponse;
    private LanguageGetByIdResponse _selectedLanguage = new();
    [Inject] public ILanguageService LanguageService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await RefreshLanguages();
    }

    private void ShowDialog(LanguageGetByIdResponse? language = null)
    {
        if (language is null)
            _selectedLanguage = new LanguageGetByIdResponse();
        else
            _selectedLanguage = new LanguageGetByIdResponse
            {
                Id = language.Id,
                Name = language.Name
            };

        _otherResponse = null;
        _isDialogOpen = true;
    }

    private void CloseDialog()
    {
        _isDialogOpen = false;
        _selectedLanguage = new LanguageGetByIdResponse();
        _otherResponse = null;
    }

    private async Task SubmitLanguage()
    {
        _otherResponse = null;
        if (_selectedLanguage.Id == 0)
        {
            var request = new LanguageCreateRequest { Name = _selectedLanguage.Name };
            _otherResponse = await LanguageService.CreateAsync(request);
        }
        else
        {
            var request = new LanguageUpdateRequest { Id = _selectedLanguage.Id, Name = _selectedLanguage.Name };
            _otherResponse = await LanguageService.UpdateAsync(request);
        }

        if (_otherResponse.IsSuccess)
        {
            await RefreshLanguages();
            CloseDialog();
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
